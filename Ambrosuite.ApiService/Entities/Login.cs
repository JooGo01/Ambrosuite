using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Ambrosuite.ApiService.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Ambrosuite.Web.Entities
{
    public class LoginModel : PageModel
    {
        private readonly DataContext _context;

        public LoginModel(DataContext context)
        {
            _context = context;
        }

        [BindProperty]
        public LoginViewModel LoginData { get; set; }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            // Buscar el usuario por CUIL en la base de datos
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.email == LoginData.email && u.baja == 0);

            var passwordHash = "";
            using (var sha256 = SHA256.Create())
            {
                passwordHash = BitConverter.ToString(sha256.ComputeHash(Encoding.UTF8.GetBytes(LoginData.password))).Replace("-", "").ToLowerInvariant();
            }

            if (usuario == null || usuario.contrasenia != passwordHash)
            {
                ModelState.AddModelError(string.Empty, "CUIL o contraseña incorrectos");
                return Page();
            }

            // Crear los Claims con la información que deseas almacenar
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, usuario.id.ToString()),
            new Claim(ClaimTypes.Name, usuario.nombre),
            new Claim(ClaimTypes.Email, usuario.email),
            new Claim("CUIL", usuario.cuil),
            new Claim(ClaimTypes.Role, usuario.rol_id.ToString())
        };

            // Crear la identidad y el principal
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            // Iniciar la autenticación: se genera la cookie con los claims
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            // Redirigir a la página principal o la que requieras
            return RedirectToPage("/");
        }

        public async Task<IActionResult> OnPostLogoutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToPage("/Login");
        }
    }
}
