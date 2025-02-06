using System.Security.Claims;
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
                .FirstOrDefaultAsync(u => u.cuil == LoginData.CUIL && u.baja == 0);

            if (usuario == null || usuario.contrasenia != LoginData.Password)
            {
                ModelState.AddModelError(string.Empty, "CUIL o contraseña incorrectos");
                return Page();
            }

            // Crear los Claims con la información que deseas almacenar
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, usuario.id.ToString()),
            new Claim(ClaimTypes.Name, usuario.nombre),
            new Claim("CUIL", usuario.cuil),
            new Claim(ClaimTypes.Role, usuario.rol_id.ToString())
        };

            // Crear la identidad y el principal
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            // Iniciar la autenticación: se genera la cookie con los claims
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            // Redirigir a la página principal o la que requieras
            return RedirectToPage("/Index");
        }

        public async Task<IActionResult> OnPostLogoutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToPage("/Account/Login");
        }
    }
}
