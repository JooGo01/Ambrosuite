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
    public class CajasController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CajasController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<CajaDTO>>> GetAllCajas()
        {
            var cajas = await _context.Cajas.ToListAsync();
            var cajasDto = _mapper.Map<IEnumerable<CajaDTO>>(cajas);
            return Ok(cajasDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CajaDTO>> GetCaja(int id)
        {
            var caja = await _context.Cajas.FindAsync(id);
            if (caja == null)
            {
                return NotFound("Caja no encontrada");
            }
            var cajaDto = _mapper.Map<CajaDTO>(caja);
            return Ok(cajaDto);
        }

        [HttpPost]
        public async Task<ActionResult<CajaDTO>> AddCaja(CajaCreateUpdateDTO cajaDto)
        {
            var caja = _mapper.Map<Caja>(cajaDto);
            _context.Cajas.Add(caja);
            await _context.SaveChangesAsync();

            return Ok(cajaDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CajaDTO>> UpdateCaja(int id, CajaCreateUpdateDTO cajaDto)
        {
            var caja = await _context.Cajas.FindAsync(id);
            if (caja == null)
            {
                return NotFound("Caja no encontrada");
            }

            _mapper.Map(cajaDto, caja);
            await _context.SaveChangesAsync();

            return Ok(cajaDto);
        }

    }
}
