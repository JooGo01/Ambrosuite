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
    public class CategoriaGastosController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CategoriaGastosController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoriaGastoDTO>>> GetAllCategoriaGastos()
        {
            var categoriaGasto = await _context.CategoriaGastos.ToListAsync();
            var categoriasGastosDto = _mapper.Map<IEnumerable<CategoriaGastoDTO>>(categoriaGasto);
            return Ok(categoriasGastosDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriaGastoDTO>> GetCategoriaGasto(int id)
        {
            var mesa = await _context.Mesas.FindAsync(id);
            if (mesa == null)
            {
                return NotFound("Categoria Gasto no encontrada");
            }
            var mesaDto = _mapper.Map<MesaDTO>(mesa);
            return Ok(mesaDto);
        }

        [HttpPost]
        public async Task<ActionResult<CategoriaGastoDTO>> AddCategoriaGasto(CategoriaGastoCreateUpdateDTO categoriaGastoDto)
        {
            var categoriaGasto = _mapper.Map<CategoriaGasto>(categoriaGastoDto);
            _context.CategoriaGastos.Add(categoriaGasto);
            await _context.SaveChangesAsync();

            return Ok(categoriaGastoDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CategoriaGastoDTO>> UpdateCategoriaGasto(int id, CategoriaGastoCreateUpdateDTO categoriaGastoDto)
        {
            var categoriaGasto = await _context.CategoriaGastos.FindAsync(id);
            if (categoriaGasto == null)
            {
                return NotFound("Categoria Gasto no encontrada");
            }

            _mapper.Map(categoriaGastoDto, categoriaGasto);
            await _context.SaveChangesAsync();

            return Ok(categoriaGasto);
        }
    }
}
