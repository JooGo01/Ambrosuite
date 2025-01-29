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
    public class MetodoPagosController : ControllerBase
    {
        // GET: MetodoPagosController
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public MetodoPagosController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<MetodoPagoDTO>>> GetAllMetodos()
        {
            var metodos = await _context.MetodoPagos.ToListAsync();
            var metodosDto = _mapper.Map<IEnumerable<MetodoPagoDTO>>(metodos);
            return Ok(metodosDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MetodoPagoDTO>> GetMetodo(int id)
        {
            var metodo = await _context.MetodoPagos.FindAsync(id);
            if (metodo == null)
            {
                return NotFound("Metodo Pago no encontrada");
            }
            var metodoDto = _mapper.Map<MetodoPagoDTO>(metodo);
            return Ok(metodoDto);
        }

        [HttpPost]
        public async Task<ActionResult<MetodoPagoDTO>> AddMesa(MetodoPagoCreateUpdateDTO metodoDto)
        {
            var metodo = _mapper.Map<MetodoPago>(metodoDto);
            _context.MetodoPagos.Add(metodo);
            await _context.SaveChangesAsync();

            return Ok(metodoDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<MetodoPagoDTO>> UpdateMesa(int id, MetodoPagoCreateUpdateDTO metodoDto)
        {
            var metodo = await _context.MetodoPagos.FindAsync(id);
            if (metodo == null)
            {
                return NotFound("Metodo Pago no encontrada");
            }

            _mapper.Map(metodoDto, metodo);
            await _context.SaveChangesAsync();

            return Ok(metodoDto);
        }
    }
}
