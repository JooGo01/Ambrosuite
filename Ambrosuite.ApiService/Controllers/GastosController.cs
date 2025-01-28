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
    public class GastosController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public GastosController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<Gasto>>> GetAllGasto()
        {
            var gastos = await _context.Gastos.Include(p => p.categoria_gasto).Include(p => p.usuario).Include(p=>p.caja).ToListAsync();
            return Ok(gastos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Gasto>> GetGasto(int id)
        {
            var gasto = await _context.Gastos.Include(p => p.categoria_gasto).Include(p => p.usuario).Include(p => p.caja).FirstOrDefaultAsync(p => p.id == id);
            if (gasto == null)
            {
                return NotFound("Pedido no encontrado");
            }
            return Ok(gasto);
        }

        [HttpGet("Caja/{cajaId}")]
        public async Task<ActionResult<List<Pedido>>> GetGastosByCaja(int cajaId)
        {
            var gastos = await _context.Gastos.Include(p => p.categoria_gasto)
                .Include(p => p.usuario)
                .Include(p => p.caja)
                .Where(p => p.caja_id == cajaId)
                .ToListAsync();

            return Ok(gastos);
        }

        [HttpGet("CategoriaGasto/{categoriaGastoId}")]
        public async Task<ActionResult<List<Pedido>>> GetGastosByCategoriaGasto(int categoriaGastoId)
        {
            var gastos = await _context.Gastos.Include(p => p.categoria_gasto)
                .Include(p => p.usuario)
                .Include(p => p.caja)
                .Where(p => p.categoria_gasto_id == categoriaGastoId)
                .ToListAsync();

            return Ok(gastos);
        }

        [HttpPost]
        public async Task<ActionResult<Gasto>> AddGasto(GastoCreateUpdateDTO gastoDto)
        {
            var gasto = _mapper.Map<Gasto>(gastoDto);
            gasto.estado = 0;
            gasto.fecha = DateTime.Now;
            _context.Gastos.Add(gasto);
            await _context.SaveChangesAsync();
            var gastoResultDto = _mapper.Map<GastoDTO>(gastoDto);
            return Ok(gastoResultDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Gasto>> UpdateGastos(int id, GastoCreateUpdateDTO gastoDto)
        {
            var gasto = await _context.Gastos.Include(p => p.categoria_gasto)
                .Include(p => p.usuario)
                .Include(p => p.caja).FirstOrDefaultAsync(p => p.id == id);
            if (gasto == null)
            {
                return NotFound("Pedido no encontrado");
            }

            _mapper.Map(gastoDto, gasto);
            await _context.SaveChangesAsync();
            var gastoActualizadoDto = _mapper.Map<GastoDTO>(gastoDto);
            return Ok(gastoActualizadoDto);
        }
    }
}
