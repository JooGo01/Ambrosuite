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
    public class CajaPedidosController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CajaPedidosController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<CajaPedido>>> GetAllCajaPedidos()
        {
            var cajapedidos = await _context.CajaPedidos.Include(p => p.caja).Include(p => p.pedido).ToListAsync();
            return Ok(cajapedidos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CajaPedido>> GetCajaPedidos(int id)
        {
            var cajapedido = await _context.CajaPedidos.Include(p => p.caja).Include(p => p.pedido).FirstOrDefaultAsync(p => p.id == id);
            if (cajapedido == null)
            {
                return NotFound("Caja Pedido no encontrado");
            }
            return Ok(cajapedido);
        }

        [HttpGet("Pedido/{pedidoId}")]
        public async Task<ActionResult<List<CajaPedido>>> GetCajaPedidoByPedido(int pedidoId)
        {
            var cajapedidos = await _context.CajaPedidos.Include(p => p.caja)
                .Include(p => p.pedido)
                .Where(p => p.pedido_id == pedidoId)
                .ToListAsync();

            return Ok(cajapedidos);
        }

        [HttpGet("Caja/{cajaId}")]
        public async Task<ActionResult<List<CajaPedido>>> GetCajaPedidoByCaja(int cajaId)
        {
            var cajapedidos = await _context.CajaPedidos.Include(p => p.caja)
                .Include(p => p.pedido)
                .Where(p => p.caja_id == cajaId)
                .ToListAsync();

            return Ok(cajapedidos);
        }

        [HttpPost]
        public async Task<ActionResult<CajaPedido>> AddCajaPedido(CajaPedidoCreateUpdateDTO cajaPedidoDto)
        {
            var cajapedido = _mapper.Map<CajaPedido>(cajaPedidoDto);
            cajapedido.fecha_hora = DateTime.Now;
            _context.CajaPedidos.Add(cajapedido);
            await _context.SaveChangesAsync();
            var cajaPedidoResultDto = _mapper.Map<CajaPedidoDTO>(cajaPedidoDto);
            return Ok(cajaPedidoResultDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CajaPedido>> UpdateCajaPedido(int id, CajaPedidoCreateUpdateDTO cajaPedidoDto)
        {
            var cajapedido = await _context.CajaPedidos.Include(p => p.caja)
                .Include(p => p.pedido)
                .FirstOrDefaultAsync(p => p.id == id);
            if (cajapedido == null)
            {
                return NotFound("Pedido no encontrado");
            }

            _mapper.Map(cajaPedidoDto, cajapedido);
            await _context.SaveChangesAsync();
            var cajaPedidoActualizadoDto = _mapper.Map<CajaPedidoDTO>(cajaPedidoDto);
            return Ok(cajaPedidoActualizadoDto);
        }
    }
}
