using Ambrosuite.ApiService.Entities;
using Ambrosuite.ApiService.EntitiesDTO;
using System.Diagnostics;

namespace Ambrosuite.Web.ServicesWeb
{
    public class MesasService
    {
        private readonly HttpClient _httpClient;

        public MesasService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            //_httpClient.BaseAddress = new Uri("https://localhost:5001");
            _httpClient.BaseAddress = new Uri("http://localhost:5000");
        }

        public async Task<List<Mesa>> GetTablesAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<Mesa>>("/api/Mesas")
                    ?? new List<Mesa>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching tables: {ex.Message}");
                throw;
            }
        }

        public async Task updateTableAsync(Mesa mesa)
        {
            try
            {
                MesaCreateUpdateDTO mesaUpdate = new MesaCreateUpdateDTO
                {
                    estado = mesa.estado
                };
                var response = await _httpClient.PutAsJsonAsync("/api/Mesas/" + mesa.id, mesaUpdate);
                Debug.WriteLine(response);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating table: {ex.Message}");
                throw;
            }
        }

        public async Task createTableAsync()
        {
            try
            {
                MesaCreateUpdateDTO mesaCreate = new MesaCreateUpdateDTO
                {
                    estado = 0
                };
                var response = await _httpClient.PostAsJsonAsync("/api/Mesas", mesaCreate);
                Debug.WriteLine(response);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex) { 
                Debug.WriteLine($"Error creating table: {ex.Message}");
            }
        }
    }
}
