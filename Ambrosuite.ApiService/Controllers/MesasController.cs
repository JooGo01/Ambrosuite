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
    public class MesasController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public MesasController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<MesaDTO>>> GetAllMesas()
        {
            var mesas = await _context.Mesas.ToListAsync();
            var mesasDto = _mapper.Map<IEnumerable<MesaDTO>>(mesas);
            return Ok(mesasDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MesaDTO>> GetMesa(int id)
        {
            var mesa = await _context.Mesas.FindAsync(id);
            if (mesa == null)
            {
                return NotFound("Mesa no encontrada");
            }
            var mesaDto = _mapper.Map<MesaDTO>(mesa);
            return Ok(mesaDto);
        }

        [HttpPost]
        public async Task<ActionResult<MesaDTO>> AddMesa(MesaCreateUpdateDTO mesaDto)
        {
            var mesa = _mapper.Map<Mesa>(mesaDto);
            _context.Mesas.Add(mesa);
            await _context.SaveChangesAsync();

            return Ok(mesaDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<MesaDTO>> UpdateMesa(int id, MesaCreateUpdateDTO mesaDto)
        {
            var mesa = await _context.Mesas.FindAsync(id);
            if (mesa == null)
            {
                return NotFound("Mesa no encontrada");
            }

            _mapper.Map(mesaDto, mesa);
            await _context.SaveChangesAsync();

            return Ok(mesaDto);
        }
    }
}
