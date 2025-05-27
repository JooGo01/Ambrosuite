using Ambrosuite.ApiService.Data;
using Microsoft.AspNetCore.Mvc;
using Ambrosuite.Web.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;

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
            var passwordHash = "";
            using (var sha256 = SHA256.Create())
            {
                passwordHash = BitConverter.ToString(sha256.ComputeHash(Encoding.UTF8.GetBytes(model.password))).Replace("-", "").ToLowerInvariant();
            }
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.email == model.email && u.baja == 0);

            if (usuario == null || usuario.contrasenia != passwordHash)
            {
                return Unauthorized(new { message = "Credenciales incorrectas" });
            }

            return Ok(new
            {
                usuario.id,
                usuario.rol_id,
                message = "Login exitoso",
                token = "ambrosuite-static-token"
            });

        }
    }
}