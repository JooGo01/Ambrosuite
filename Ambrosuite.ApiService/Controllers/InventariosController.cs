using Ambrosuite.ApiService.Data;
using Ambrosuite.ApiService.Entities;
using Ambrosuite.ApiService.EntitiesDTO;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ambrosuite.ApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventariosController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public InventariosController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<Inventario>>> GetAllInventario()
        {
            var inventarios = await _context.Inventarios.Where(p=>p.stock>0).Include(p => p.ingrediente).ToListAsync();
            return Ok(inventarios);
        }

        [HttpGet("todos")]
        public async Task<ActionResult<List<Inventario>>> GetAllInventarioTodos()
        {
            var inventarios = await _context.Inventarios.Include(p => p.ingrediente).ToListAsync();
            return Ok(inventarios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Inventario>> GetInventario(int id)
        {
            var inventario = await _context.Inventarios.Include(p => p.ingrediente).FirstOrDefaultAsync(p => p.id == id);
            if (inventario == null)
            {
                return NotFound("Inventario no encontrado");
            }
            return Ok(inventario);
        }

        [HttpGet("Ingrediente/{ingredienteId}")]
        public async Task<ActionResult<List<Inventario>>> GetPedidosByIngrediente(int ingredienteId)
        {
            var inventarios = await _context.Inventarios
                .Include(p => p.ingrediente)
                .Where(p => p.ingrediente_id == ingredienteId)
                .ToListAsync();
            return Ok(inventarios);
        }

        [HttpPost]
        public async Task<ActionResult<Inventario>> AddInventario(InventarioCreateUpdateDTO inventarioDto)
        {
            var inventario = _mapper.Map<Inventario>(inventarioDto);
            _context.Inventarios.Add(inventario);
            await _context.SaveChangesAsync();
            var inventarioResultDto = _mapper.Map<InventarioDTO>(inventarioDto);
            return Ok(inventarioResultDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Inventario>> UpdateInventario(int id, InventarioCreateUpdateDTO inventarioDto)
        {
            var inventario = await _context.Inventarios.Include(p => p.ingrediente).FirstOrDefaultAsync(p => p.id == id);
            if (inventario == null)
            {
                return NotFound("Inventario no encontrado");
            }

            _mapper.Map(inventarioDto, inventario);
            await _context.SaveChangesAsync();
            var inventarioActualizadoDto = _mapper.Map<InventarioDTO>(inventario);
            return Ok(inventarioActualizadoDto);
        }

        [HttpPost("{pedidoId}/actualizarstock")]
        public async Task<IActionResult> ActualizarStockIngredientes(int pedidoId)
        {
            try
            {
                using (var context = _context)
                {
                // Obtener los productos del pedido
                    var productosDelPedido = await context.PedidoDetalles
                    .Where(pd => pd.pedido_id == pedidoId && pd.estado == 0)
                    .ToListAsync();

                    foreach (var detalle in productosDelPedido)
                    {
                        // Obtener los ingredientes de cada producto
                        var productos = await context.ProductosFinales
                            .Where(r => r.id == detalle.producto_id && r.estado == 0)
                            .ToListAsync();
                        foreach (var producto in productos)
                        {
                            var recetas = await context.Recetas
                                .Where(r => r.producto_final_id == producto.id && r.estado == 0)
                                .ToListAsync();

                            foreach (var receta in recetas)
                            {
                                var ingredientes = await context.Ingredientes
                                    .Where(i => i.id == receta.ingrediente_id && i.estado == 0)
                                    .ToListAsync();
                                foreach (var ingrediente in ingredientes)
                                {
                                    double cantidadNecesaria = (double)(receta.cantidad * detalle.cantidad);
    
                                    // Obtener inventarios disponibles para el ingrediente, ordenados por menor stock
                                    var inventarios = await context.Inventarios
                                        .Where(i => i.ingrediente_id == ingrediente.id && i.stock > 0)
                                        .OrderBy(i => i.stock) // Ordenar de menor a mayor
                                        .ToListAsync();

                                    foreach (var inventario in inventarios)
                                    {
                                        if (cantidadNecesaria <= 0) break; // Si ya se cubrió la necesidad, salir
        
                                        if (inventario.stock >= cantidadNecesaria)
                                        {
                                            // Descontar lo necesario y salir
                                            inventario.stock -= cantidadNecesaria;
                                            cantidadNecesaria = 0;
                                        }
                                        else
                                        {
                                            // Consumir todo el stock de este registro y continuar con el siguiente
                                            cantidadNecesaria -= (double)inventario.stock;
                                            inventario.stock = 0;
                                        }
                                    }
    
                                    if (cantidadNecesaria > 0)
                                    {
                                        return BadRequest(new { error = $"Stock insuficiente para el ingrediente {ingrediente.id}" }); 
                                    }
                                }
                            }
                        }
                    }
    
                    // Guardar cambios en la base de datos
                    await context.SaveChangesAsync();
                    return Ok(new { mensaje = "Stock actualizado correctamente." });
                }
            }catch (Exception ex){
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
