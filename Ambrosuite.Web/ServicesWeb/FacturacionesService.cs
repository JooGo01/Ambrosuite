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
            //_httpClient.BaseAddress = new Uri("https://localhost:5001");
            _httpClient.BaseAddress = new Uri("http://localhost:5000");
        }

        public async Task agregarFacturacionAsync(Facturacion p_facturacion, int pedidoId)
        {
            try
            {
                // Obtener el último número de factura
                var ultimoNumeroFacturaResponse = await _httpClient.GetAsync("/api/Facturaciones/ultimo-numero-factura");
                ultimoNumeroFacturaResponse.EnsureSuccessStatusCode();
                var ultimoNumeroFactura = await ultimoNumeroFacturaResponse.Content.ReadAsStringAsync();

                var pedidoDetallesResponse = await _httpClient.GetAsync($"/api/PedidoDetalles/pedidoAct/{pedidoId}");
                pedidoDetallesResponse.EnsureSuccessStatusCode();
                var pedidoDetalles = await pedidoDetallesResponse.Content.ReadFromJsonAsync<List<PedidoDetalle>>();

                // Calcular el total a partir de los detalles del pedido
                double total = (double)pedidoDetalles.Sum(detalle => detalle.cantidad * detalle.producto.precio);

                FacturacionCreateUpdateDTO facturacionCreateUpdateDTO = new FacturacionCreateUpdateDTO
                {
                    fecha_emision = p_facturacion.fecha_emision,
                    total = total,
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

                if (facturacion.id > 0) { 
                    var pedidoResponse = await _httpClient.GetAsync($"/api/Pedidos/{pedidoId}");
                    pedidoResponse.EnsureSuccessStatusCode();
                    var pedido = await pedidoResponse.Content.ReadFromJsonAsync<Pedido>();

                    PedidoCreateUpdateDTO pedidoDto = new PedidoCreateUpdateDTO
                    {
                        mesa_id = pedido.mesa_id,
                        usuario_id = pedido.usuario_id,
                        total = total,
                        estado = 1
                    };

                    var responsePedido = await _httpClient.PutAsJsonAsync($"/api/Pedidos/{pedidoId}", pedidoDto);
                    Debug.WriteLine(responsePedido);
                    responsePedido.EnsureSuccessStatusCode();
                }

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

        public async Task<List<Facturacion>> GetFacturasAsync(DateTime? dia, DateTime? mes, int? anio)
        {
            var query = new List<string>();

            if (dia.HasValue)
            {
                query.Add($"dia={dia.Value.ToString("yyyy-MM-dd")}");
            }

            if (mes.HasValue)
            {
                query.Add($"mes={mes.Value.ToString("yyyy-MM")}");
            }

            if (anio.HasValue)
            {
                query.Add($"anio={anio.Value}");
            }

            var queryString = string.Join("&", query);
            var response = await _httpClient.GetAsync($"api/Facturaciones/filtro-consulta?{queryString}");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<List<Facturacion>>();
        }
    }
}
