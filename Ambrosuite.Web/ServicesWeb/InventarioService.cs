using Ambrosuite.ApiService.Entities;
using Ambrosuite.ApiService.EntitiesDTO;
using System.Diagnostics;
using System.Text;

namespace Ambrosuite.Web.ServicesWeb
{
    public class InventarioService
    {
        private readonly HttpClient _httpClient;

        public InventarioService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:5001");
        }

        public async Task<List<Inventario>> GetInventariosAsync()
        {
            try
            {
                List<Inventario> listadoInventario = new List<Inventario>();
                listadoInventario = await _httpClient.GetFromJsonAsync<List<Inventario>>("/api/Inventarios")
                    ?? new List<Inventario>();
                return listadoInventario;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching inventario: {ex.Message}");
                throw;
            }
        }

        public async Task updateInventarioAsync(Inventario p_inventario)
        {
            try
            {
                InventarioCreateUpdateDTO inventarioCreateUpdateDTO = new InventarioCreateUpdateDTO
                {
                    lote = p_inventario.lote,
                    stock = p_inventario.stock,
                    fecha_vencimiento = p_inventario.fecha_vencimiento,
                    ingrediente_id = p_inventario.ingrediente_id
                };
                var response = await _httpClient.PutAsJsonAsync("/api/Inventarios/" + p_inventario.id, inventarioCreateUpdateDTO);
                Debug.WriteLine(response);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating inventario: {ex.Message}");
                throw;
            }
        }

        public async Task deleteInventarioAsync(Inventario p_inventario)
        {
            try
            {
                InventarioCreateUpdateDTO inventarioCreateUpdateDTO = new InventarioCreateUpdateDTO
                {
                    lote = p_inventario.lote,
                    stock = 0,
                    fecha_vencimiento = p_inventario.fecha_vencimiento,
                    ingrediente_id = p_inventario.ingrediente_id
                };
                var response = await _httpClient.PutAsJsonAsync("/api/Inventarios/" + p_inventario.id, inventarioCreateUpdateDTO);
                Debug.WriteLine(response);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting inventario: {ex.Message}");
                throw;
            }
        }

        public async Task agregarInventarioAsync(Inventario p_inventario)
        {
            try
            {
                InventarioCreateUpdateDTO inventarioCreateUpdateDTO = new InventarioCreateUpdateDTO
                {
                    lote = p_inventario.lote,
                    stock = p_inventario.stock,
                    fecha_vencimiento = p_inventario.fecha_vencimiento,
                    ingrediente_id = p_inventario.ingrediente_id
                };

                var response = await _httpClient.PostAsJsonAsync("/api/Inventarios", inventarioCreateUpdateDTO);
                Debug.WriteLine(response);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error agregando inventario: {ex.Message}");
                throw;
            }
        }
    }
}
