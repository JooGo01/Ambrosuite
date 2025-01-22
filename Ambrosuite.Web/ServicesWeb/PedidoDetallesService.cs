using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Ambrosuite.ApiService.Entities;

namespace Ambrosuite.Web.ServicesWeb
{
    public class PedidoDetalleService
    {
        private readonly HttpClient _httpClient;

        public PedidoDetalleService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:5001");
        }

        public async Task<List<PedidoDetalle>> GetDetallePedidoByIdAsync(int pedidoId) {
            List<PedidoDetalle>? pedido = await _httpClient.GetFromJsonAsync<List<PedidoDetalle>>($"api/pedidodetalles/pedido/{pedidoId}");
            return pedido;
        }
    }
}
