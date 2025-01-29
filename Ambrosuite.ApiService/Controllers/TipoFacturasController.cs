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
    public class TipoFacturasController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public TipoFacturasController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<TipoFacturaDTO>>> GetAllTipoFacturas()
        {
            var tipofacturas = await _context.TipoFacturas.ToListAsync();
            var tipoFacturasDto = _mapper.Map<IEnumerable<TipoFacturaDTO>>(tipofacturas);
            return Ok(tipoFacturasDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TipoFacturaDTO>> GetTipoFactura(int id)
        {
            var tipofactura = await _context.TipoFacturas.FindAsync(id);
            if (tipofactura == null)
            {
                return NotFound("Metodo Pago no encontrada");
            }
            var tipoFacturasDto = _mapper.Map<TipoFacturaDTO>(tipofactura);
            return Ok(tipoFacturasDto);
        }

        [HttpPost]
        public async Task<ActionResult<TipoFacturaDTO>> AddTipoFactura(TipoFacturaCreateUpdateDTO tipoFacturaDto)
        {
            var tipoFactura = _mapper.Map<TipoFactura>(tipoFacturaDto);
            _context.TipoFacturas.Add(tipoFactura);
            await _context.SaveChangesAsync();

            return Ok(tipoFacturaDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TipoFacturaDTO>> UpdateTipoFactura(int id, TipoFacturaCreateUpdateDTO tipoFacturaDto)
        {
            var tipoFactura = await _context.TipoFacturas.FindAsync(id);
            if (tipoFactura == null)
            {
                return NotFound("Metodo Pago no encontrada");
            }

            _mapper.Map(tipoFacturaDto, tipoFactura);
            await _context.SaveChangesAsync();

            return Ok(tipoFacturaDto);
        }
    }
}
