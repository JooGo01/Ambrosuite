using Ambrosuite.ApiService.Data;
using Ambrosuite.ApiService.Entities;
using Ambrosuite.ApiService.EntitiesDTO;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace Ambrosuite.ApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoDetallesController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public PedidoDetallesController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<PedidoDetalle>>> GetAllPedidoDetalles()
        {
            var pedidoDetalles = await _context.PedidoDetalles.Include(pd => pd.producto).Include(pd => pd.pedido).ToListAsync();
            var pedidoDetalleDto = _mapper.Map<List<PedidoDetalleDTO>>(pedidoDetalles);
            return Ok(pedidoDetalleDto);
        }

        [HttpGet("pedido/{pedidoId}")]
        public async Task<ActionResult<List<PedidoDetalle>>> GetPedidoDetalleByPedido(int pedidoId)
        {
            var pedidoDetalles = await _context.PedidoDetalles
                .Include(pd => pd.producto)
                .Include(pd => pd.pedido)
                .Where(p => p.pedido_id == pedidoId && p.producto.estado==0)
                .ToListAsync();
            var pedidoDetalleDto = _mapper.Map<List<PedidoDetalleDTO>>(pedidoDetalles);
            return Ok(pedidoDetalleDto);
        }

        [HttpGet("pedidoAct/{pedidoId}")]
        public async Task<ActionResult<List<PedidoDetalle>>> GetPedidoDetalleByPedidoAct(int pedidoId)
        {
            var pedidoDetalles = await _context.PedidoDetalles
                .Include(pd => pd.producto)
                    .ThenInclude(p=> p.recetas)
                .Include(pd => pd.pedido)
                .Where(p => p.pedido_id == pedidoId && p.producto.estado == 0 && p.estado==0)
                .ToListAsync();
            var pedidoDetalleDto = _mapper.Map<List<PedidoDetalleDTO>>(pedidoDetalles);
            return Ok(pedidoDetalleDto);
        }

        [HttpGet("{pedidoId}/{productoId}")]
        public async Task<ActionResult<PedidoDetalle>> GetPedidoDetalleByPedidoAndProducto(int pedidoId, int productoId)
        {

            var pedidoDetalle = await _context.PedidoDetalles
                .Include(pd => pd.producto)
                .Include(pd => pd.pedido)
                .FirstOrDefaultAsync(p => p.pedido_id == pedidoId && p.producto_id==productoId);

            if(pedidoDetalle == null)
            {
                return Ok(pedidoDetalle= new PedidoDetalle());
            }
            return Ok(pedidoDetalle);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PedidoDetalle>> GetPedidoDetalle(int id)
        {
            var pedidoDetalle = await _context.PedidoDetalles.Include(pd => pd.producto).Include(pd => pd.pedido).FirstOrDefaultAsync(pd => pd.id == id);
            if (pedidoDetalle == null)
            {
                return NotFound("Detalle de pedido no encontrado");
            }
            var pedidoDetalleDto = _mapper.Map<PedidoDetalleDTO>(pedidoDetalle);
            return Ok(pedidoDetalleDto);
        }

        [HttpPost]
        public async Task<ActionResult<PedidoDetalleDTO>> AddPedidoDetalle(PedidoDetalleCreateUpdateDTO pedidoDetalleDto)
        {
            try {
                var pedidoDetalle = _mapper.Map<PedidoDetalle>(pedidoDetalleDto);
                _context.PedidoDetalles.Add(pedidoDetalle);
                await _context.SaveChangesAsync();
                //var pedidoDetalles = await GetAllPedidoDetalles();
                return Ok(pedidoDetalle);
            }
            catch (DbUpdateException ex) when (ex.InnerException is MySqlException mysqlEx && mysqlEx.Number == 1062)
            {
                return Conflict(new { message = "El detalle del pedido ya existe." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error al guardar el detalle del pedido.", details = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PedidoDetalleDTO>> UpdatePedidoDetalle(int id, PedidoDetalleCreateUpdateDTO pedidoDetalleDto)
        {
            var pedidoDetalle = await _context.PedidoDetalles.Include(pd => pd.producto).Include(pd => pd.pedido).FirstOrDefaultAsync(pd => pd.id == id);
            if (pedidoDetalle == null)
            {
                return NotFound("Detalle de pedido no encontrado");
            }

            _mapper.Map(pedidoDetalleDto, pedidoDetalle);
            await _context.SaveChangesAsync();

            var pedidoDetalleActualizadoDto = _mapper.Map<PedidoDetalleDTO>(pedidoDetalle);

            return Ok(pedidoDetalleActualizadoDto);
        }
    }
}
