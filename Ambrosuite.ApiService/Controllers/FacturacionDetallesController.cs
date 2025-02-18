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
    public class FacturacionDetallesController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public FacturacionDetallesController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<FacturacionDetalle>>> GetAllFacturacionDetalles()
        {
            var facturacionDetalle = await _context.FacturacionDetalles.Include(p => p.facturacion).Include(p => p.producto).ToListAsync();
            return Ok(facturacionDetalle);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FacturacionDetalle>> GetFacturacionDetalle(int id)
        {
            var facturacionDetalle = await _context.FacturacionDetalles.Include(p => p.facturacion).Include(p => p.producto).FirstOrDefaultAsync(p => p.id == id);
            if (facturacionDetalle == null)
            {
                return NotFound("Facturacion Detalle no encontrado");
            }
            return Ok(facturacionDetalle);
        }

        [HttpPost]
        public async Task<ActionResult<FacturacionDetalle>> AddFacturacioDetalle(FacturacionDetalleCreateUpdateDTO facturacionDetalleDto)
        {
            var facturaciondetalle = _mapper.Map<FacturacionDetalle>(facturacionDetalleDto);
            _context.FacturacionDetalles.Add(facturaciondetalle);
            await _context.SaveChangesAsync();
            var facturacionDetalleResultDto = _mapper.Map<FacturacionDetalleDTO>(facturacionDetalleDto);
            return Ok(facturacionDetalleResultDto);
        }

        [HttpPost("lista")]
        public async Task<ActionResult> AddFacturacionDetalleLista(List<FacturacionDetalleCreateUpdateDTO> facturacionDetalleDtos)
        {
            var facturacionDetalles = _mapper.Map<List<FacturacionDetalle>>(facturacionDetalleDtos);
            _context.FacturacionDetalles.AddRange(facturacionDetalles);
            await _context.SaveChangesAsync();
            var facturacionDetalleResultDtos = _mapper.Map<List<FacturacionDetalleDTO>>(facturacionDetalles);
            return Ok(facturacionDetalleResultDtos);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<FacturacionDetalle>> UpdateFacturacionDetalle(int id, FacturacionDetalleCreateUpdateDTO facturacionDetalleDto)
        {
            var facturaciondetalle = await _context.FacturacionDetalles.Include(p => p.facturacion).Include(p => p.producto).FirstOrDefaultAsync(p => p.id == id);
            if (facturaciondetalle == null)
            {
                return NotFound("Facturacion Detalle no encontrado");
            }

            _mapper.Map(facturacionDetalleDto, facturaciondetalle);
            await _context.SaveChangesAsync();
            var facturacionDetalleActualizadoDto = _mapper.Map<FacturacionDetalleDTO>(facturacionDetalleDto);
            return Ok(facturacionDetalleActualizadoDto);
        }
    }
}
