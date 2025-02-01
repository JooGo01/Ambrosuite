using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ambrosuite.ApiService.Entities
{
    [Table("pedido_facturacion")]
    public class PedidoFacturacion
    {
        [Key, Column(Order = 1)]
        public int pedido_id { get; set; }

        [Key, Column(Order = 2)]
        public int facturacion_id { get; set; }

        [ForeignKey("pedido_id")]
        public Pedido pedido { get; set; }

        [ForeignKey("facturacion_id")]
        public Facturacion facturacion { get; set; }
    }
}
