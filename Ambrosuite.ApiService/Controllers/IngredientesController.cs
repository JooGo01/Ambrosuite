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
    public class IngredientesController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public IngredientesController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<List<Ingrediente>>> GetAllIngredientes()
        {
            var ingredientes = await _context.Ingredientes.ToListAsync();
            return Ok(ingredientes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Ingrediente>> GetIngrediente(int id)
        {
            var ingrediente = await _context.Ingredientes.FindAsync(id);
            if (ingrediente is null)
            {
                return NotFound("Ingrediente no encontrado");
            }
            return Ok(ingrediente);
        }

        [HttpPost]
        public async Task<ActionResult<Ingrediente>> AddIngrediente(IngredienteCreateUpdateDTO ingredienteDto)
        {
            var ingrediente = _mapper.Map<Ingrediente>(ingredienteDto);
            _context.Ingredientes.Add(ingrediente);
            await _context.SaveChangesAsync();

            var ingredienteResultDto = _mapper.Map<IngredienteDTO>(ingrediente);
            return Ok(ingredienteResultDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Ingrediente>> UpdateIngrediente(int id, IngredienteCreateUpdateDTO ingredienteDto)
        {
            var ingredienteUpdate = await _context.Ingredientes.FindAsync(id);
            if (ingredienteUpdate is null)
            {
                return NotFound("Ingrediente no encontrado");
            }

            _mapper.Map(ingredienteDto, ingredienteUpdate);
            await _context.SaveChangesAsync();
            return Ok(ingredienteUpdate);
        }

        /*
        Comentado ya que vamos a utilizar una baja logica para mantener la integridad de la base de datos
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Ingrediente>>> DeleteIngrediente(int id)
        {
            var ingrediente = await _context.Ingredientes.FindAsync(id);
            if (ingrediente is null)
            {
                return NotFound("Ingrediente no encontrado");
            }

            _context.Ingredientes.Remove(ingrediente);
            await _context.SaveChangesAsync();
            var ingredientes = await GetAllIngredientes();
            return Ok(ingredientes);
        }*/

        [HttpPut("delete/{id}")]
        public async Task<ActionResult<Ingrediente>> SoftDeleteIngrediente(int id)
        {
            var ingrediente = await _context.Ingredientes.FindAsync(id);

            if (ingrediente == null)
            {
                return NotFound("Ingrediente no encontrado");
            }

            ingrediente.estado = 1;

            await _context.SaveChangesAsync();

            return Ok(ingrediente);
        }


    }
}
