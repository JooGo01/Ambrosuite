using Ambrosuite.ApiService.Entities;
using Ambrosuite.ApiService.EntitiesDTO;
using System.Diagnostics;

namespace Ambrosuite.Web.ServicesWeb
{
    public class CategoriaProductosService
    {
        private readonly HttpClient _httpClient;

        public CategoriaProductosService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:5001");
        }

        public async Task<List<CategoriaProducto>> GetCategoriaProductosAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<CategoriaProducto>>("/api/CategoriaProductos")
                    ?? new List<CategoriaProducto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching tables: {ex.Message}");
                throw;
            }
        }

        public async Task updateCategoriaProductosAsync(CategoriaProducto categoriaProducto)
        {
            try
            {
                CategoriaProductoCreateUpdateDTO categoriaProductoCreateUpdateDTO = new CategoriaProductoCreateUpdateDTO
                {
                    categoria_id = categoriaProducto.categoria_id,
                    producto_id = categoriaProducto.producto_id
                };

                var response = await _httpClient.PutAsJsonAsync("/api/CategoriaProductos/" + categoriaProducto.id, categoriaProductoCreateUpdateDTO);
                Debug.WriteLine(response);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating table: {ex.Message}");
                throw;
            }
        }

        public async Task agregarCategoriaProductosAsync(CategoriaProducto p_categoriaProducto)
        {
            try
            {
                CategoriaProductoCreateUpdateDTO categoriaProductoDTO = new CategoriaProductoCreateUpdateDTO
                {
                    categoria_id = p_categoriaProducto.categoria_id,
                    producto_id = p_categoriaProducto.producto_id
                };

                var response = await _httpClient.PostAsJsonAsync("/api/CategoriaProductos", categoriaProductoDTO);
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
