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
    public class CajaMovimientosController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CajaMovimientosController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<CajaMovimiento>>> GetAllCajaMovimientos()
        {
            var cajamovimiento = await _context.CajaMovimientos.Include(p => p.caja).Include(p => p.usuario).ToListAsync();
            return Ok(cajamovimiento);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CajaMovimiento>> GetCajaMovimientos(int id)
        {
            var cajamovimiento= await _context.CajaMovimientos.Include(p => p.caja).Include(p => p.usuario).FirstOrDefaultAsync(p => p.id == id);
            if (cajamovimiento == null)
            {
                return NotFound("Caja Movimiento no encontrado");
            }
            return Ok(cajamovimiento);
        }

        [HttpGet("Usuario/{usuarioId}")]
        public async Task<ActionResult<List<CajaMovimiento>>> GetCajaMovimientoByUsuario(int usuarioId)
        {
            var cajamovimiento = await _context.CajaMovimientos.Include(p => p.caja)
                .Include(p => p.usuario)
                .Where(p => p.usuario_id == usuarioId)
                .ToListAsync();

            return Ok(cajamovimiento);
        }

        [HttpGet("Caja/{cajaId}")]
        public async Task<ActionResult<List<CajaMovimiento>>> GetCajaMovimientoByCaja(int cajaId)
        {
            var cajamovimiento = await _context.CajaMovimientos.Include(p => p.caja)
                .Include(p => p.usuario)
                .Where(p => p.caja_id == cajaId)
                .ToListAsync();

            return Ok(cajamovimiento);
        }

        [HttpPost]
        public async Task<ActionResult<CajaMovimiento>> AddCajaMovimiento(CajaMovimientoCreateUpdateDTO cajaMovimientoDto)
        {
            var cajamovimiento = _mapper.Map<CajaMovimiento>(cajaMovimientoDto);
            cajamovimiento.fecha_hora_movimiento = DateTime.Now;
            _context.CajaMovimientos.Add(cajamovimiento);
            await _context.SaveChangesAsync();
            var cajaMovimientoResultDto = _mapper.Map<CajaMovimientoDTO>(cajaMovimientoDto);
            return Ok(cajaMovimientoResultDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CajaMovimiento>> UpdateCajaMovimiento(int id, CajaMovimientoCreateUpdateDTO cajaMovimientoDto)
        {
            var cajamovimiento = await _context.CajaMovimientos.Include(p => p.caja)
                .Include(p => p.usuario)
                .FirstOrDefaultAsync(p => p.id == id);
            if (cajamovimiento == null)
            {
                return NotFound("Caja Movimiento no encontrado");
            }

            _mapper.Map(cajaMovimientoDto, cajamovimiento);
            await _context.SaveChangesAsync();
            var cajaMovimientoActualizadoDto = _mapper.Map<CajaMovimientoDTO>(cajaMovimientoDto);
            return Ok(cajaMovimientoActualizadoDto);
        }
    }
}
