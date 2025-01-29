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
    }
}
