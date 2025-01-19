using Ambrosuite.ApiService.Entities;

namespace Ambrosuite.Web.ServicesWeb
{
    public class MesasService
    {
        private readonly HttpClient _httpClient;

        public MesasService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:5001");
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
    }
}
