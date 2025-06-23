using Ambrosuite.ApiService.Data;
using Ambrosuite.ApiService.Entities;
using Ambrosuite.ApiService.EntitiesDTO;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ambrosuite.ApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosFinalesController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ProductosFinalesController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductoFinal>>> GetAllProductosFinales()
        {
            var productosFinales = await _context.ProductosFinales.Where(p=>p.estado==0).ToListAsync();
            return Ok(productosFinales);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductoFinal>> GetProductoFinal(int id)
        {
            var productoFinal = await _context.ProductosFinales.FindAsync(id);
            if (productoFinal is null)
            {
                return NotFound("Producto final no encontrado");
            }
            return Ok(productoFinal);
        }

        [HttpPost]
        public async Task<ActionResult<ProductoFinal>> AddProductoFinal(ProductoFinalCreateUpdateDTO productoFinalDto)
        {
            var productoFinal = _mapper.Map<ProductoFinal>(productoFinalDto);
            _context.ProductosFinales.Add(productoFinal);
            await _context.SaveChangesAsync();

            var productoFinalResultDto = _mapper.Map<ProductoFinalDTO>(productoFinal);
            return Ok(productoFinalResultDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProductoFinal>> UpdateProductoFinal(int id, ProductoFinalCreateUpdateDTO productoFinalDto)
        {
            var productoFinalUpdate = await _context.ProductosFinales.FindAsync(id);
            if (productoFinalUpdate is null)
            {
                return NotFound("Producto final no encontrado");
            }

            _mapper.Map(productoFinalDto, productoFinalUpdate);
            await _context.SaveChangesAsync();
            var productoFinalActualizadoDto = _mapper.Map<ProductoFinalDTO>(productoFinalUpdate);
            return Ok(productoFinalActualizadoDto);
        }
        /*
        Comentado debido a que se van a implementar bajas logicas
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<ProductoFinal>>> DeleteProductoFinal(int id)
        {
            var productoFinal = await _context.ProductosFinales.FindAsync(id);
            if (productoFinal is null)
            {
                return NotFound("Producto final no encontrado");
            }

            _context.ProductosFinales.Remove(productoFinal);
            await _context.SaveChangesAsync();
            var productosFinales = await GetAllProductosFinales();
            return Ok(productosFinales);
        }
        */

        [HttpPut("delete/{id}")]
        public async Task<ActionResult<ProductoFinal>> SoftDeleteProductoFinal(int id)
        {
            var productoFinal = await _context.ProductosFinales.FindAsync(id);

            if (productoFinal == null)
            {
                return NotFound("Ingrediente no encontrado");
            }

            productoFinal.estado = 1;

            await _context.SaveChangesAsync();

            return Ok(productoFinal);
        }
    }
}
