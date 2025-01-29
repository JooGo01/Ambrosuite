using Ambrosuite.ApiService.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ambrosuite.ApiService.EntitiesDTO
{
    public class PedidoFacturacionDTO
    {
        public int pedido_id { get; set; }
        public int facturacion_id { get; set; }

        public PedidoDTO pedido { get; set; }
        public FacturacionDTO facturacion { get; set; }
    }
}
