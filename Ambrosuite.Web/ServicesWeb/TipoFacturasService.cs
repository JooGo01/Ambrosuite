using Ambrosuite.ApiService.Entities;
using Ambrosuite.ApiService.EntitiesDTO;
using System.Diagnostics;

namespace Ambrosuite.Web.ServicesWeb
{
    public class TipoFacturasService
    {
        private readonly HttpClient _httpClient;

        public TipoFacturasService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:5001");
        }

        public async Task<List<TipoFactura>> GetTipoFacturaAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<TipoFactura>>("/api/TipoFacturas")
                    ?? new List<TipoFactura>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching tables: {ex.Message}");
                throw;
            }
        }
    }
}
