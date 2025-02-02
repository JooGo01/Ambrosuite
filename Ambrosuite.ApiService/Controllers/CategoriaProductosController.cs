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
    public class CategoriaProductosController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CategoriaProductosController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoriaProductoDTO>>> GetAllCategoriaProducto()
        {
            var categoriaProducto = await _context.CategoriaProductos.ToListAsync();
            var categoriasProductoDto = _mapper.Map<IEnumerable<CategoriaProductoDTO>>(categoriaProducto);
            return Ok(categoriasProductoDto);
        }

        [HttpPost]
        public async Task<ActionResult<CategoriaProductoDTO>> AddCategoriaProducto(CategoriaProductoCreateUpdateDTO categoriaProductoDto)
        {
            var categoriaProducto = _mapper.Map<CategoriaProducto>(categoriaProductoDto);
            _context.CategoriaProductos.Add(categoriaProducto);
            await _context.SaveChangesAsync();

            return Ok(categoriaProductoDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CategoriaProductoDTO>> UpdateCategoriaProducto(int id, CategoriaProductoCreateUpdateDTO categoriaProductoDto)
        {
            var categoriaProducto = await _context.CategoriaProductos.FindAsync(id);
            if (categoriaProducto == null)
            {
                return NotFound("Categoria Producto no encontrada");
            }

            _mapper.Map(categoriaProductoDto, categoriaProducto);
            await _context.SaveChangesAsync();

            return Ok(categoriaProducto);
        }
    }
}
