using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Ambrosuite.ApiService.Entities;

namespace Ambrosuite.Web.ServicesWeb
{
    public class PedidosService
    {
        private readonly HttpClient _httpClient;

        public PedidosService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:5001");
        }

        public async Task<List<Pedido>> GetPedidosByMesaAsync(int mesaId)
        {
            List<Pedido>? pedidos = await _httpClient.GetFromJsonAsync<List<Pedido>>($"api/pedidos/mesa/{mesaId}");
            return pedidos;
        }

        public async Task<List<Pedido>> GetPedidosByActivosAsync()
        {
            List<Pedido>? pedidos = await _httpClient.GetFromJsonAsync<List<Pedido>>($"api/pedidos/activos");
            return pedidos;
        }

        public async Task<Pedido> GetPedidoByIdAsync(int pedidoId) {
            Pedido? pedido = await _httpClient.GetFromJsonAsync<Pedido>($"api/pedidos/{pedidoId}");
            return pedido;
        }
    }
}
