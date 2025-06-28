using Ambrosuite.ApiService.Entities;
using Ambrosuite.ApiService.EntitiesDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;

namespace Ambrosuite.Web.ServicesWeb
{
    public class CategoriaGastosService
    {
        private readonly HttpClient _httpClient;

        public CategoriaGastosService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            //_httpClient.BaseAddress = new Uri("https://localhost:5001");
            _httpClient.BaseAddress = new Uri("http://localhost:5000");
        }

        public async Task<List<CategoriaGasto>> GetCategoriaGastosAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<CategoriaGasto>>("/api/CategoriaGastos")
                    ?? new List<CategoriaGasto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching tables: {ex.Message}");
                throw;
            }
        }

        public async Task updateCategoriaGastoAsync(CategoriaGasto categoriaGasto)
        {
            try
            {
                CategoriaGastoCreateUpdateDTO categoriaGastoCreateUpdateDTO = new CategoriaGastoCreateUpdateDTO
                {
                    gasto_nombre = categoriaGasto.gasto_nombre,
                    gasto_descripcion = categoriaGasto.gasto_descripcion
                };

                var response = await _httpClient.PutAsJsonAsync("/api/CategoriaGastos/" + categoriaGasto.id, categoriaGastoCreateUpdateDTO);
                Debug.WriteLine(response);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating table: {ex.Message}");
                throw;
            }
        }

        public async Task agregarCategoriaGastosAsync(CategoriaGasto p_categoriaGasto)
        {
            try
            {
                CategoriaGastoCreateUpdateDTO rol = new CategoriaGastoCreateUpdateDTO
                {
                    gasto_nombre = p_categoriaGasto.gasto_nombre,
                    gasto_descripcion = p_categoriaGasto.gasto_descripcion
                };

                var response = await _httpClient.PostAsJsonAsync("/api/CategoriaGastos", p_categoriaGasto);
                Debug.WriteLine(response);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding table: {ex.Message}");
                throw;
            }
        }
    }
}
