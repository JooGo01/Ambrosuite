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
    public class PedidosController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public PedidosController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<Pedido>>> GetAllPedidos()
        {
            var pedidos = await _context.Pedidos.Include(p => p.mesa).Include(p => p.usuario).ToListAsync();
            return Ok(pedidos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pedido>> GetPedido(int id)
        {
            var pedido = await _context.Pedidos.Include(p => p.mesa).Include(p => p.usuario).FirstOrDefaultAsync(p => p.id == id);
            if (pedido == null)
            {
                return NotFound("Pedido no encontrado");
            }
            return Ok(pedido);
        }

        [HttpGet("activos")]
        public async Task<ActionResult<List<Pedido>>> GetPedidosActivos()
        {
            var pedidos = await _context.Pedidos
                .Include(p => p.mesa)
                .Include(p => p.usuario)
                .Where(p => p.estado == 0)
                .ToListAsync();
            return Ok(pedidos);
        }

        [HttpGet("activo/{id}")]
        public async Task<ActionResult<Pedido>> GetPedidosActivoId(int id)
        {
            var pedido = await _context.Pedidos.Include(p => p.mesa).Include(p => p.usuario).Where(p => p.estado == 0 || p.estado == 1).FirstOrDefaultAsync(p => p.id == id);
            if (pedido == null)
            {
                return NotFound("Pedido no encontrado");
            }
            return Ok(pedido);
        }

        [HttpGet("mesa/{mesaId}")]
        public async Task<ActionResult<List<Pedido>>> GetPedidosByMesa(int mesaId)
        {
            var pedidos = await _context.Pedidos
                .Include(p => p.mesa)
                .Include(p => p.usuario)
                .Where(p => p.mesa_id == mesaId)
                .ToListAsync();
            /*var pedidos = await _context.Pedidos
                .Include(p => p.mesa)
                .Include(p => p.usuario)
                .Where(p => p.mesa_id == mesaId && p.estado==0)
                .ToListAsync();*/

            return Ok(pedidos);
        }

        [HttpGet("mesaact/{mesaId}")]
        public async Task<ActionResult<List<Pedido>>> GetPedidosActByMesa(int mesaId)
        {
            var pedidos = await _context.Pedidos
                .Include(p => p.mesa)
                .Include(p => p.usuario)
                .Where(p => p.mesa_id == mesaId && p.estado==0 && p.mesa.estado==1)
                .ToListAsync();

            return Ok(pedidos);
        }

        [HttpPost]
        public async Task<ActionResult<Pedido>> AddPedido(PedidoCreateUpdateDTO pedidoDto)
        {
            var pedido = _mapper.Map<Pedido>(pedidoDto);
            pedido.estado = 0;
            pedido.fecha_hora= DateTime.Now;
            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();
            var pedidoResultDto = _mapper.Map<PedidoDTO>(pedidoDto);
            return Ok(pedidoResultDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Pedido>> UpdatePedido(int id, PedidoCreateUpdateDTO pedidoDto)
        {
            var pedido = await _context.Pedidos.Include(p => p.mesa).Include(p => p.usuario).FirstOrDefaultAsync(p => p.id == id);
            if (pedido == null)
            {
                return NotFound("Pedido no encontrado");
            }

            _mapper.Map(pedidoDto, pedido);
            await _context.SaveChangesAsync();
            var pedidoActualizadoDto = _mapper.Map<PedidoDTO>(pedido);
            return Ok(pedidoActualizadoDto);
        }

        [HttpPut("estadoPedido/{id}")]
        public async Task<ActionResult<Pedido>> UpdatePedidoFacturado(int id, PedidoCreateUpdateDTO pedidoDto)
        {
            var pedido = await _context.Pedidos.FirstOrDefaultAsync(p => p.id == id);
            if (pedido == null)
            {
                return NotFound("Pedido no encontrado");
            }

            _mapper.Map(pedidoDto, pedido);
            await _context.SaveChangesAsync();
            var pedidoActualizadoDto = _mapper.Map<PedidoDTO>(pedido);
            return Ok(pedidoActualizadoDto);
        }
    }
}
