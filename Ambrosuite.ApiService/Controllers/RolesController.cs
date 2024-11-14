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
    public class RolesController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public RolesController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<List<Rol>>> GetAllRoles(){
            var roles = await _context.Roles.ToListAsync();
            return Ok(roles);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Rol>> GetRol(int id)
        {
            var rol = await _context.Roles.FindAsync(id);
            if (rol is null)
            {
                return NotFound("Rol no encontrado");
            }
            return Ok(rol);
        }

        [HttpPost]
        public async Task<ActionResult<Rol>> AddRol(RolCreateUpdateDTO rolDto)
        {
            var rol = _mapper.Map<Rol>(rolDto);
            _context.Roles.Add(rol);
            await _context.SaveChangesAsync();

            var rolResultDto = _mapper.Map<RolDTO>(rol);
            return Ok(rolResultDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Rol>> UpdateRol(int id, RolCreateUpdateDTO rolDto)
        {
            var rolUpdate = await _context.Roles.FindAsync(id);
            if (rolUpdate is null)
            {
                return NotFound("Rol no encontrado");
            }

            _mapper.Map(rolDto, rolUpdate);
            await _context.SaveChangesAsync();
            var rolActualizadoDto = _mapper.Map<RolDTO>(rolUpdate);
            return Ok(rolActualizadoDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Rol>>> DeleteRol(int id)
        {
            var rol = await _context.Roles.FindAsync(id);
            if (rol is null)
            {
                return NotFound("Rol no encontrado");
            }
            _context.Roles.Remove(rol);
            await _context.SaveChangesAsync();
            var roles = await GetAllRoles();
            return Ok(roles);
        }
    }
}
