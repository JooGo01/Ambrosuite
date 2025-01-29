using Ambrosuite.ApiService.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ambrosuite.ApiService.EntitiesDTO
{
    public class PedidoFacturacionCreateUpdateDTO
    {
        public int pedido_id { get; set; }
        public int facturacion_id { get; set; }
    }
}
