using Ambrosuite.ApiService.Entities;
using Ambrosuite.ApiService.EntitiesDTO;
using System.Diagnostics;

namespace Ambrosuite.Web.ServicesWeb
{
    public class RolesService
    {
        private readonly HttpClient _httpClient;

        public RolesService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:5001");
        }

        public async Task<List<Rol>> GetRolesAsync()
        {
            try
            {
                List<Rol> listadoRol = new List<Rol>();
                listadoRol = await _httpClient.GetFromJsonAsync<List<Rol>>("/api/Roles")
                    ?? new List<Rol>();
                return listadoRol;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching tables: {ex.Message}");
                throw;
            }
        }

        public async Task updateRolAsync(Rol p_rol)
        {
            try
            {
                RolCreateUpdateDTO rol = new RolCreateUpdateDTO
                {
                    nombre_rol = p_rol.nombre_rol,
                    descripcion_rol = p_rol.descripcion_rol
                };

                var response = await _httpClient.PutAsJsonAsync("/api/Roles/" + p_rol.id, rol);
                Debug.WriteLine(response);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating table: {ex.Message}");
                throw;
            }
        }

        public async Task deleteRolAsync(Rol p_rol)
        {
            try
            {
                var response = await _httpClient.DeleteAsync("/api/Roles/" + p_rol.id);
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
