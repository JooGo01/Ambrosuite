using Ambrosuite.ApiService.Data;
using Microsoft.AspNetCore.Mvc;
using Ambrosuite.Web.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ambrosuite.ApiService.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AutenticacionController : ControllerBase
    {
        private readonly DataContext _context;

        public AutenticacionController(DataContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.cuil == model.CUIL && u.baja == 0);

            if (usuario == null || usuario.contrasenia != model.Password)
            {
                return Unauthorized(new { message = "Credenciales incorrectas" });
            }

            return Ok(new { message = "Login exitoso" });
        }
    }
}