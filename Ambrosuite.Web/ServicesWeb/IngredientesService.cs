using Ambrosuite.ApiService.Entities;
using Ambrosuite.ApiService.EntitiesDTO;
using System.Diagnostics;

namespace Ambrosuite.Web.ServicesWeb
{
    public class IngredientesService
    {
        private readonly HttpClient _httpClient;

        public IngredientesService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:5001");
        }

        public async Task<List<Ingrediente>> GetIngredienteAsync()
        {
            try
            {
                List<Ingrediente> listadoIngrediente = new List<Ingrediente>();
                listadoIngrediente = await _httpClient.GetFromJsonAsync<List<Ingrediente>>("/api/Ingredientes")
                    ?? new List<Ingrediente>();
                return listadoIngrediente;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching tables: {ex.Message}");
                throw;
            }
        }

        public async Task updateIngredienteAsync(Ingrediente p_ingrediente)
        {
            try
            {
                IngredienteCreateUpdateDTO ingrediente = new IngredienteCreateUpdateDTO
                {
                    nombre = p_ingrediente.nombre,
                    descripcion = p_ingrediente.descripcion,
                    marca = p_ingrediente.marca,
                    alertaCantidad = p_ingrediente.alertaCantidad,
                    estado=0
                };

                var response = await _httpClient.PutAsJsonAsync("/api/Ingredientes/" + p_ingrediente.id, ingrediente);
                Debug.WriteLine(response);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating table: {ex.Message}");
                throw;
            }
        }

        public async Task deleteIngredienteAsync(Ingrediente p_ingrediente)
        {
            try
            {
                IngredienteCreateUpdateDTO ingrediente = new IngredienteCreateUpdateDTO
                {
                    nombre = p_ingrediente.nombre,
                    descripcion = p_ingrediente.descripcion,
                    marca = p_ingrediente.marca,
                    alertaCantidad = p_ingrediente.alertaCantidad,
                    estado = 1
                };

                var response = await _httpClient.PutAsJsonAsync("/api/Ingredientes/" + p_ingrediente.id, ingrediente);
                Debug.WriteLine(response);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating table: {ex.Message}");
                throw;
            }
        }

        public async Task agregarIngredienteAsync(Ingrediente p_ingrediente)
        {
            try
            {
                IngredienteCreateUpdateDTO ingrediente = new IngredienteCreateUpdateDTO
                {
                    nombre = p_ingrediente.nombre,
                    descripcion = p_ingrediente.descripcion,
                    marca = p_ingrediente.marca,
                    alertaCantidad = p_ingrediente.alertaCantidad,
                    estado = 0
                };

                var response = await _httpClient.PostAsJsonAsync("/api/Ingredientes", ingrediente);
                Debug.WriteLine(response);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating table: {ex.Message}");
                throw;
            }
        }
    }
}
