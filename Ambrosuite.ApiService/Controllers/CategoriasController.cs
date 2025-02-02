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
    public class CategoriasController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CategoriasController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoriaDTO>>> GetAllCategoriaActivos()
        {
            var categoria = await _context.Categorias.Where(p => p.estado == 0).ToListAsync();
            var categoriasDto = _mapper.Map<IEnumerable<CategoriaDTO>>(categoria);
            return Ok(categoriasDto);
        }

        [HttpGet("todos")]
        public async Task<ActionResult<List<CategoriaDTO>>> GetAllCategoria()
        {
            var categoria= await _context.Categorias.ToListAsync();
            var categoriasDto = _mapper.Map<IEnumerable<CategoriaDTO>>(categoria);
            return Ok(categoriasDto);
        }

        [HttpPost]
        public async Task<ActionResult<CategoriaDTO>> AddCategoriaProducto(CategoriaCreateUpdateDTO categoriaDto)
        {
            var categoria = _mapper.Map<Categoria>(categoriaDto);
            _context.Categorias.Add(categoria);
            await _context.SaveChangesAsync();

            return Ok(categoriaDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CategoriaDTO>> UpdateCategoriaProducto(int id, CategoriaCreateUpdateDTO categoriaDto)
        {
            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null)
            {
                return NotFound("Categoria no encontrada");
            }

            _mapper.Map(categoriaDto, categoria);
            await _context.SaveChangesAsync();

            return Ok(categoria);
        }
    }
}
