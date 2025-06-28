using Ambrosuite.ApiService.Entities;
using Ambrosuite.ApiService.EntitiesDTO;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;

namespace Ambrosuite.Web.ServicesWeb
{
    public class UsuariosService
    {
        private readonly HttpClient _httpClient;

        public UsuariosService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            //_httpClient.BaseAddress = new Uri("https://localhost:5001");
            _httpClient.BaseAddress = new Uri("http://localhost:5000");
        }

        public async Task<List<Usuario>> GetUsuariosAsync()
        {
            try
            {
                List<Usuario> listadoUsuario = new List<Usuario>();
                listadoUsuario= await _httpClient.GetFromJsonAsync<List<Usuario>>("/api/Usuarios")
                    ?? new List<Usuario>();
                return listadoUsuario;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching tables: {ex.Message}");
                throw;
            }
        }

        public async Task updateUsuarioAsync(Usuario p_usuario)
        {
            try
            {
                UsuarioCreateUpdateDTO usuario = new UsuarioCreateUpdateDTO
                {
                    nombre = p_usuario.nombre,
                    apellido = p_usuario.apellido,
                    fecha_nacimiento = p_usuario.fecha_nacimiento,
                    email = p_usuario.email,
                    cuil = p_usuario.cuil,
                    contrasenia = p_usuario.contrasenia,
                    baja = p_usuario.baja,
                    rol_id = p_usuario.rol_id
                };
                var response = await _httpClient.PutAsJsonAsync("/api/Usuarios/" + p_usuario.id, usuario);
                Debug.WriteLine(response);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating table: {ex.Message}");
                throw;
            }
        }

        public async Task deleteUsuarioAsync(Usuario p_usuario) {
            try
            {
                var response = await _httpClient.PutAsJsonAsync("/api/Usuarios/delete/" + p_usuario.id, p_usuario.id);
                Debug.WriteLine(response);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting table: {ex.Message}");
                throw;
            }
        }

        public async Task agregarUsuarioAsync(Usuario p_usuario)
        {
            try
            {
                using var sha256 = SHA256.Create();
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(p_usuario.contrasenia));
                string hashedPassword = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

                UsuarioCreateUpdateDTO usuario = new UsuarioCreateUpdateDTO
                {
                    nombre = p_usuario.nombre,
                    apellido = p_usuario.apellido,
                    fecha_nacimiento = p_usuario.fecha_nacimiento,
                    email = p_usuario.email,
                    cuil = p_usuario.cuil,
                    contrasenia = hashedPassword,
                    baja = p_usuario.baja,
                    rol_id = p_usuario.rol_id
                };

                var response = await _httpClient.PostAsJsonAsync("/api/Usuarios", usuario);
                Debug.WriteLine(response);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding table: {ex.Message}");
                throw;
            }
        }

        public async Task modificarUsuarioAsync(Usuario p_usuario)
        {
            try
            {
                // Obtener el usuario actual desde la API para comparar la contraseña
                var usuarioActual = await _httpClient.GetFromJsonAsync<Usuario>($"/api/Usuarios/{p_usuario.id}");

                if (usuarioActual == null)
                {
                    throw new Exception("Usuario no encontrado");
                }

                // Verificar si la contraseña ha cambiado
                string hashedPassword;
                if (usuarioActual.contrasenia != p_usuario.contrasenia)
                {
                    using var sha256 = SHA256.Create();
                    byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(p_usuario.contrasenia));
                    hashedPassword = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
                }
                else
                {
                    hashedPassword = usuarioActual.contrasenia;
                }

                UsuarioCreateUpdateDTO usuario = new UsuarioCreateUpdateDTO
                {
                    nombre = p_usuario.nombre,
                    apellido = p_usuario.apellido,
                    fecha_nacimiento = p_usuario.fecha_nacimiento,
                    email = p_usuario.email,
                    cuil = p_usuario.cuil,
                    contrasenia = hashedPassword,
                    baja = p_usuario.baja,
                    rol_id = p_usuario.rol_id
                };

                var response = await _httpClient.PutAsJsonAsync($"/api/Usuarios/{p_usuario.id}", usuario);
                Debug.WriteLine(response);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error modifying table: {ex.Message}");
                throw;
            }
        }

        /*
        public async Task<Usuario> LoginAsync(string email, string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var passwordHash = BitConverter.ToString(sha256.ComputeHash(Encoding.UTF8.GetBytes(password))).Replace("-", "").ToLowerInvariant();
                return await _context.Usuarios.FirstOrDefaultAsync(u => u.email == email && u.contrasenia == passwordHash);
            }
        }
        */
    }
}
