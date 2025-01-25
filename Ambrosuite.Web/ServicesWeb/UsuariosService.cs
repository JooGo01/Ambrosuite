using Ambrosuite.ApiService.Entities;
using Ambrosuite.ApiService.EntitiesDTO;
using System.Diagnostics;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;

namespace Ambrosuite.Web.ServicesWeb
{
    public class UsuariosService
    {
        private readonly HttpClient _httpClient;

        public UsuariosService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:5001");
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
    }
}
