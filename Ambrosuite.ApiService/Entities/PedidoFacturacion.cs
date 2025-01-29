using System.ComponentModel.DataAnnotations.Schema;

namespace Ambrosuite.ApiService.Entities
{
    [Table("pedido_facturacion")]
    public class PedidoFacturacion
    {
        public int pedido_id { get; set; }
        public int facturacion_id { get; set; }

        [ForeignKey("pedido_id")]
        public Pedido pedido { get; set; }

        [ForeignKey("facturacion_id")]
        public Facturacion facturacion { get; set; }
    }
}
