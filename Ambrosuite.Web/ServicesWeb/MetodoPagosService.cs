using Ambrosuite.ApiService.Entities;
using Ambrosuite.ApiService.EntitiesDTO;
using System.Diagnostics;

namespace Ambrosuite.Web.ServicesWeb
{
    public class MetodoPagosService
    {
        private readonly HttpClient _httpClient;

        public MetodoPagosService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:5001");
        }

        public async Task<List<MetodoPago>> GetMetodoPagoAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<MetodoPago>>("/api/MetodoPagos")
                    ?? new List<MetodoPago>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching tables: {ex.Message}");
                throw;
            }
        }
    }
}
