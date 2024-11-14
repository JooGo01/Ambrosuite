using Ambrosuite.ApiService.Data;
using Ambrosuite.ApiService.Entities;
using Ambrosuite.ApiService.EntitiesDTO;
using Ambrosuite.ApiService.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ambrosuite.ApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IUsuarioService _usuarioService;

        public UsuariosController(DataContext context, IMapper mapper, IUsuarioService usuarioService)
        {
            _context = context;
            _mapper = mapper;
            _usuarioService = usuarioService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistroDTO usuarioDto)
        {
            try
            {
                var usuario = await _usuarioService.CrearUsuarioAsync(usuarioDto);
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = "Error al registrar usuario", detalle = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            var usuario = await _usuarioService.LoginAsync(loginDTO.email, loginDTO.contrasenia);

            if (usuario == null)
            {
                return Unauthorized(new { mensaje = "Credenciales incorrectas" });
            }

            return Ok(usuario);
        }

        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> GetAllUsuarios() {
            //var usuarios = await _context.Usuarios.ToListAsync();
            var usuarios = await _context.Usuarios.Include(u => u.Rol).ToListAsync();
            var usuariosDto = _mapper.Map<IEnumerable<UsuarioDTO>>(usuarios);
            return Ok(usuariosDto);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Usuario>>> GetUsuario(int id)
        {
            var usuarioBuscado = await _context.Usuarios.FindAsync(id);
            if (usuarioBuscado is null) {
                return NotFound("Usuario no encontrado");
            }
            var usuario = _mapper.Map<UsuarioDTO>(usuarioBuscado);
            return Ok(usuario);
        }
        [HttpPost]
        public async Task<ActionResult<UsuarioDTO>> AddUsuario(UsuarioCreateUpdateDTO usuarioDto)
        {
            var usuario = _mapper.Map<Usuario>(usuarioDto);
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            var usuarios = await GetAllUsuarios();
            return Ok(usuarios);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Usuario>> UpdateUsuario(int id, UsuarioCreateUpdateDTO usuarioDto)
        {
            var usuarioUpdate = await _context.Usuarios.Include(u => u.Rol).FirstOrDefaultAsync(u => u.id == id);
            if (usuarioUpdate is null)
            {
                return NotFound("Usuario no encontrado");
            }
            
            _mapper.Map(usuarioDto, usuarioUpdate);
            await _context.SaveChangesAsync();
            var usuarioActualizadoDto = _mapper.Map<UsuarioDTO>(usuarioUpdate);
            return Ok(usuarioActualizadoDto);
        }
        /*
        Comentado debido a que se van a implementar bajas logicas
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Usuario>>> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario is null)
            {
                return NotFound("Usuario no encontrado");
            }
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            var usuarios = await GetAllUsuarios();
            return Ok(usuarios);
        }
        */
        [HttpPut("delete/{id}")]
        public async Task<ActionResult<Usuario>> SoftDeleteUsuario(int id)
        {
            var usuario= await _context.Usuarios.Include(u => u.Rol).FirstOrDefaultAsync(u => u.id == id);

            if (usuario == null)
            {
                return NotFound("Ingrediente no encontrado");
            }

            usuario.baja = 1;

            await _context.SaveChangesAsync();

            return Ok(usuario);
        }
    }
}
