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
    public class FacturacionesController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public FacturacionesController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<Facturacion>>> GetAllFacturaciones()
        {
            var pedidos = await _context.Facturaciones.Include(p => p.tipoFactura).Include(p => p.metodoPago).ToListAsync();
            return Ok(pedidos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Facturacion>> GetFacturacion(int id)
        {
            var pedido = await _context.Facturaciones.Include(p => p.tipoFactura).Include(p => p.metodoPago).FirstOrDefaultAsync(p => p.id == id);
            if (pedido == null)
            {
                return NotFound("Facturacion no encontrado");
            }
            return Ok(pedido);
        }

        [HttpPost]
        public async Task<ActionResult<Facturacion>> AddFacturacion(FacturacionCreateUpdateDTO facturacionDto)
        {
            var facturacion = _mapper.Map<Facturacion>(facturacionDto);
            facturacion.estado = 0;
            facturacion.fecha_emision = DateTime.Now;
            _context.Facturaciones.Add(facturacion);
            await _context.SaveChangesAsync();
            return Ok(facturacion);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Facturacion>> UpdateFacturacion(int id, FacturacionCreateUpdateDTO facturacionDto)
        {
            var facturacion = await _context.Facturaciones.Include(p => p.tipoFactura).Include(p => p.metodoPago).FirstOrDefaultAsync(p => p.id == id);
            if (facturacion == null)
            {
                return NotFound("Facturacion no encontrado");
            }

            _mapper.Map(facturacionDto, facturacion);
            await _context.SaveChangesAsync();
            var factuacionActualizadoDto = _mapper.Map<FacturacionDTO>(facturacion);
            return Ok(factuacionActualizadoDto);
        }

        [HttpGet("ultimo-id")]
        public async Task<ActionResult<int>> GetUltimoId()
        {
            var ultimoId = await _context.Facturaciones.MaxAsync(f => (int?)f.id) ?? 0;
            return Ok(ultimoId);
        }

        [HttpGet("ultimo-numero-factura")]
        public async Task<ActionResult<string>> GetUltimoNumeroFactura()
        {
            var ultimoNumeroFactura = await _context.Facturaciones.MaxAsync(f => f.numero_factura);
            if (ultimoNumeroFactura == null)
            {
                return Ok("00000001");
            }
            else
            {
                var nuevoNumeroFactura = (ultimoNumeroFactura.Value + 1).ToString("D8");
                return Ok(nuevoNumeroFactura);
            }
        }
    }
}
