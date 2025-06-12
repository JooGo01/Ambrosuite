using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Ambrosuite.ApiService.Entities;
using Ambrosuite.ApiService.EntitiesDTO;

namespace Ambrosuite.Web.ServicesWeb
{
    public class FacturacionDetalleService
    {
        private readonly HttpClient _httpClient;

        public FacturacionDetalleService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:5001");
        }
        public async Task agregarFacturacionDetalleAsync(List<PedidoDetalle> p_pedidoDetalle, int pedidoId)
        {
            try
            {
                var facturacionPedidoResponse = await _httpClient.GetAsync("/api/PedidoFacturaciones/pedido/" + pedidoId);
                facturacionPedidoResponse.EnsureSuccessStatusCode();
                var facturacionPedido = await facturacionPedidoResponse.Content.ReadFromJsonAsync<PedidoFacturacion>();

                if (facturacionPedido == null)
                {
                    throw new Exception("No se encontró la facturación asociada al pedido.");
                }

                //cambiar estado a "limpieza" de la mesa 


                // Construir la lista de FacturacionDetalleCreateUpdateDTO
                var facturacionDetalles = new List<FacturacionDetalleCreateUpdateDTO>();

                foreach (var pedidoDetalle in p_pedidoDetalle)
                {
                    var facturacionDetalleDto = new FacturacionDetalleCreateUpdateDTO
                    {
                        producto_id = pedidoDetalle.producto_id,
                        facturacion_id = facturacionPedido.facturacion_id,
                        tipo_unidad = "unidad", // Asumiendo un valor por defecto, ajusta según sea necesario
                        unidad = pedidoDetalle.cantidad ?? 0
                    };

                    facturacionDetalles.Add(facturacionDetalleDto);
                }

                // Enviar la lista de FacturacionDetalleCreateUpdateDTO al servidor
                var response = await _httpClient.PostAsJsonAsync("/api/FacturacionDetalles/lista", facturacionDetalles);
                Debug.WriteLine(response);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error agregando Pedido Detalle: {ex.Message}");
                throw;
            }
        }
    }
}
