using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Ambrosuite.ApiService.Entities;
using Ambrosuite.ApiService.EntitiesDTO;

namespace Ambrosuite.Web.ServicesWeb
{
    public class FacturacionesService
    {
        private readonly HttpClient _httpClient;

        public FacturacionesService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:5001");
        }

        public async Task agregarFacturacionAsync(Facturacion p_facturacion, int pedidoId)
        {
            try
            {
                // Obtener el último número de factura
                var ultimoNumeroFacturaResponse = await _httpClient.GetAsync("/api/Facturaciones/ultimo-numero-factura");
                ultimoNumeroFacturaResponse.EnsureSuccessStatusCode();
                var ultimoNumeroFactura = await ultimoNumeroFacturaResponse.Content.ReadAsStringAsync();

                FacturacionCreateUpdateDTO facturacionCreateUpdateDTO = new FacturacionCreateUpdateDTO
                {
                    fecha_emision = p_facturacion.fecha_emision,
                    total = p_facturacion.total,
                    punto_venta = p_facturacion.punto_venta,
                    numero_factura = int.Parse(ultimoNumeroFactura),
                    metodo_pago_id= p_facturacion.metodo_pago_id,
                    tipo_factura_id = p_facturacion.tipo_factura_id
                };

                var response = await _httpClient.PostAsJsonAsync("/api/Facturaciones", facturacionCreateUpdateDTO);
                Debug.WriteLine(response);
                response.EnsureSuccessStatusCode();

                // Segunda solicitud POST para agregar otra entidad relacionada (ejemplo: PedidoFacturacion)
                var facturacion = await response.Content.ReadFromJsonAsync<Facturacion>();
                var pedidoFacturacion = new PedidoFacturacion
                {
                    pedido_id = pedidoId, // Asumiendo que el id del pedido está en p_facturacion
                    facturacion_id = facturacion.id
                };

                var responsePedidoFacturacion = await _httpClient.PostAsJsonAsync("/api/PedidoFacturaciones", pedidoFacturacion);
                Debug.WriteLine(responsePedidoFacturacion);
                responsePedidoFacturacion.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating table: {ex.Message}");
                throw;
            }
        }
    }
}
