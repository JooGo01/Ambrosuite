using Ambrosuite.ApiService.Data;
using Ambrosuite.ApiService.Entities;
using Ambrosuite.ApiService.EntitiesDTO;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Ambrosuite.ApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecetasController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public RecetasController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<Receta>>> GetAllRecetas()
        {
            var recetas = await _context.Recetas.Include(p=> p.ingrediente).Include(p=>p.producto_final).Where(p=>p.estado==0).ToListAsync();
            //var recetas = await _context.Recetas
            //    .Include(r => r.ingrediente)
            //    .Include(r => r.productoFinal)
            //    .ToListAsync();
            /*
            var query = _context.Recetas
                .Include(r => r.ingrediente)
                .Include(r => r.producto_final)
                .ToQueryString();
            Debug.WriteLine(query);
            var queryy = _context.Recetas
                .ToQueryString();
            Debug.WriteLine(queryy);
            */
            return Ok(recetas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Receta>> GetReceta(int id)
        {
            var receta = await _context.Recetas
                .Include(r => r.ingrediente)
                .Include(r => r.producto_final)
                .FirstOrDefaultAsync(p => p.id == id);

            if (receta is null)
            {
                return NotFound("Receta no encontrada");
            }
            return Ok(receta);
        }

        [HttpPost]
        public async Task<ActionResult<Receta>> AddReceta(RecetaCreateUpdateDTO recetaDto)
        {
            var receta = _mapper.Map<Receta>(recetaDto);
            _context.Recetas.Add(receta);
            await _context.SaveChangesAsync();
            return Ok(recetaDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Receta>> UpdateReceta(int id, RecetaCreateUpdateDTO recetaDto)
        {
            var recetaUpdate = await _context.Recetas.FirstOrDefaultAsync(p => p.id == id);
            if (recetaUpdate is null)
            {
                return NotFound("Receta no encontrada");
            }

            _mapper.Map(recetaDto, recetaUpdate);
            await _context.SaveChangesAsync();
            return Ok(recetaUpdate);
        }

        /*
        [HttpPut("{productoFinalId}/{ingredienteId}/{id}")]
        public async Task<ActionResult<Receta>> UpdateReceta(int productoFinalId, int ingredienteId, int id, RecetaCreateUpdateDTO recetaDto)
        {
            var recetaUpdate = await _context.Recetas.FindAsync(id, productoFinalId, ingredienteId);
            if (recetaUpdate is null)
            {
                return NotFound("Receta no encontrada");
            }

            _mapper.Map(recetaDto, recetaUpdate);
            await _context.SaveChangesAsync();
            var recetaActualizadoDto = _mapper.Map<RecetaDTO>(recetaDto);
            return Ok(recetaActualizadoDto);
        }
        */

        /*
        Comentado debido a que se va a utilizar bajas logicas en lugar de fisicas para mantener la integridad de la base de datos
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Receta>>> DeleteReceta(int id)
        {
            var receta = await _context.Recetas.FindAsync(id);
            if (receta is null)
            {
                return NotFound("Receta no encontrada");
            }

            _context.Recetas.Remove(receta);
            await _context.SaveChangesAsync();
            var recetas = await GetAllRecetas();
            return Ok(recetas);
        }
        */
        [HttpPut("delete/{productoFinalId}/{ingredienteId}/{id}")]
        public async Task<ActionResult<Receta>> SoftDeleteReceta(int productoFinalId, int ingredienteId, int id)
        {
            var receta = await _context.Recetas.FindAsync(id, productoFinalId, ingredienteId);

            if (receta == null)
            {
                return NotFound("Ingrediente no encontrado");
            }

            receta.estado = 1;

            await _context.SaveChangesAsync();
            return Ok(receta);
        }

    }
}
