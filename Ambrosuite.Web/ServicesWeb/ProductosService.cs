using Ambrosuite.ApiService.Entities;
using Ambrosuite.ApiService.EntitiesDTO;
using System.Diagnostics;

namespace Ambrosuite.Web.ServicesWeb
{
    public class ProductosService
    {
        private readonly HttpClient _httpClient;

        public ProductosService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:5001");
        }

        public async Task<List<ProductoFinal>> GetProductosAsync()
        {
            try
            {
                List<ProductoFinal> listadoProductos = new List<ProductoFinal>();
                listadoProductos = await _httpClient.GetFromJsonAsync<List<ProductoFinal>>("/api/ProductosFinales")
                    ?? new List<ProductoFinal>();
                return listadoProductos;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching tables: {ex.Message}");
                throw;
            }
        }

        public async Task updateProductoAsync(ProductoFinal p_producto)
        {
            try
            {
                ProductoFinalCreateUpdateDTO productoFinalCreateUpdateDTO = new ProductoFinalCreateUpdateDTO
                {
                    nombre = p_producto.nombre,
                    descripcion = p_producto.descripcion,
                    precio = p_producto.precio,
                    estado = 0
                };

                var response = await _httpClient.PutAsJsonAsync("/api/ProductosFinales/" + p_producto.id, productoFinalCreateUpdateDTO);
                Debug.WriteLine(response);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating table: {ex.Message}");
                throw;
            }
        }

        public async Task deleteProductoAsync(ProductoFinal p_producto)
        {
            try
            {
                ProductoFinalCreateUpdateDTO productoFinalCreateUpdateDTO = new ProductoFinalCreateUpdateDTO
                {
                    nombre = p_producto.nombre,
                    descripcion = p_producto.descripcion,
                    precio = p_producto.precio,
                    estado = 1
                };


                var response = await _httpClient.PutAsJsonAsync("/api/ProductosFinales/" + p_producto.id, productoFinalCreateUpdateDTO);
                Debug.WriteLine(response);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating table: {ex.Message}");
                throw;
            }
        }

        public async Task agregarProductoAsync(ProductoFinal p_producto)
        {
            try
            {
                ProductoFinalCreateUpdateDTO productoFinalCreateUpdateDTO = new ProductoFinalCreateUpdateDTO
                {
                    nombre = p_producto.nombre,
                    descripcion = p_producto.descripcion,
                    precio = p_producto.precio,
                    estado = 0
                };

                var response = await _httpClient.PostAsJsonAsync("/api/ProductosFinales", productoFinalCreateUpdateDTO);
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
