using Ambrosuite.ApiService.Entities;
using Ambrosuite.ApiService.EntitiesDTO;
using System.Diagnostics;

namespace Ambrosuite.Web.ServicesWeb
{
    public class RecetaService
    {
        private readonly HttpClient _httpClient;

        public RecetaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:5001");
        }

        public async Task<List<Receta>> GetRecetaAsync()
        {
            try
            {
                List<Receta> recetas = new List<Receta>();
                recetas = await _httpClient.GetFromJsonAsync<List<Receta>>("/api/Recetas") ?? new List<Receta>();
                return recetas;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching receta: {ex.Message}");
                throw;
            }
        }

        public async Task updateRecetaAsync(Receta p_receta)
        {
            try
            {
                RecetaCreateUpdateDTO recetaCreateUpdateDTO = new RecetaCreateUpdateDTO
                {
                    producto_final_id = p_receta.producto_final_id,
                    ingrediente_id = p_receta.ingrediente_id,
                    cantidad = p_receta.cantidad,
                    descripcion=p_receta.descripcion,
                    estado=0
                };

                var response = await _httpClient.PutAsJsonAsync("/api/Recetas/" + p_receta.id, recetaCreateUpdateDTO);
                Debug.WriteLine(response);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating receta: {ex.Message}");
                throw;
            }
        }

        public async Task bajaRecetaAsync(Receta p_receta)
        {
            try
            {
                RecetaCreateUpdateDTO recetaCreateUpdateDTO = new RecetaCreateUpdateDTO
                {
                    producto_final_id = p_receta.producto_final_id,
                    ingrediente_id = p_receta.ingrediente_id,
                    cantidad = p_receta.cantidad,
                    descripcion = p_receta.descripcion,
                    estado=1
                };

                var response = await _httpClient.PutAsJsonAsync("/api/Recetas/" + p_receta.id, recetaCreateUpdateDTO);
                Debug.WriteLine(response);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating receta: {ex.Message}");
                throw;
            }
        }

        public async Task agregarRecetaAsync(Receta p_receta)
        {
            try
            {
                RecetaCreateUpdateDTO recetaCreateUpdateDTO = new RecetaCreateUpdateDTO
                {
                    producto_final_id = p_receta.producto_final_id,
                    ingrediente_id = p_receta.ingrediente_id,
                    cantidad = p_receta.cantidad,
                    descripcion = p_receta.descripcion,
                    estado = 0
                };
                var response = await _httpClient.PostAsJsonAsync("/api/Recetas", recetaCreateUpdateDTO);
                Debug.WriteLine(response);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding receta: {ex.Message}");
                throw;
            }
        }
    }
}
