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
    public class PedidoFacturacionesController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public PedidoFacturacionesController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<List<PedidoFacturacion>>> GetAllPedidoFacturaciones()
        {
            var pedidoFacturaciones = await _context.PedidoFacturaciones.Include(p => p.pedido).Include(p => p.facturacion).ToListAsync();
            return Ok(pedidoFacturaciones);
        }
        [HttpGet("facturacion/{facturacionId}")]
        public async Task<ActionResult<PedidoFacturacion>> GetPedidoFacturacionByFacturacion(int facturacionId)
        {
            var pedidoFacturacion = await _context.PedidoFacturaciones.Include(p => p.pedido).Include(p => p.facturacion).FirstOrDefaultAsync(p => p.facturacion_id == facturacionId);
            if (pedidoFacturacion == null)
            {
                return NotFound("PedidoFacturacion no encontrado");
            }
            return Ok(pedidoFacturacion);
        }
        [HttpGet("pedido/{pedidoId}")]
        public async Task<ActionResult<PedidoFacturacion>> GetPedidoFacturacionByPedido(int pedidoId)
        {
            var pedidoFacturacion = await _context.PedidoFacturaciones.Include(p => p.pedido).Include(p => p.facturacion).FirstOrDefaultAsync(p => p.pedido_id == pedidoId);
            if (pedidoFacturacion == null)
            {
                return NotFound("PedidoFacturacion no encontrado");
            }
            return Ok(pedidoFacturacion);
        }
        [HttpPost]
        public async Task<ActionResult<PedidoFacturacion>> AddPedidoFacturacion(PedidoFacturacionCreateUpdateDTO pedidoFacturacionDto)
        {
            var pedidoFacturacion = _mapper.Map<PedidoFacturacion>(pedidoFacturacionDto);
            _context.PedidoFacturaciones.Add(pedidoFacturacion);
            await _context.SaveChangesAsync();
            var pedidoFacturacionResultDto = _mapper.Map<PedidoFacturacionDTO>(pedidoFacturacion);
            return Ok(pedidoFacturacionResultDto);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<PedidoFacturacion>> UpdatePedidoFacturacion(int id, PedidoFacturacionCreateUpdateDTO pedidoFacturacionDto)
        {
            var pedidoFacturacion = await _context.PedidoFacturaciones.Include(p => p.facturacion).Include(p => p.pedido)
                .Where(p => p.pedido_id == pedidoFacturacionDto.pedido_id && p.facturacion_id==pedidoFacturacionDto.facturacion_id)
                .ToListAsync();
            if (pedidoFacturacion == null)
            {
                return NotFound("PedidoFacturacion no encontrado");
            }
            _mapper.Map(pedidoFacturacionDto, pedidoFacturacion);
            await _context.SaveChangesAsync();
            var pedidoFacturacionActualizadoDto = _mapper.Map<PedidoFacturacionDTO>(pedidoFacturacion);
            return Ok(pedidoFacturacionActualizadoDto);
        }
    }
}
