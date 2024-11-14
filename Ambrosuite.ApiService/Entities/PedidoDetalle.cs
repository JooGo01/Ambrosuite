using System.ComponentModel.DataAnnotations.Schema;

namespace Ambrosuite.ApiService.Entities
{
    [Table("pedido_detalle")]
    public class PedidoDetalle
    {
        public int id { get; set; }
        public int producto_id { get; set; }
        public int pedido_id { get; set; }
        public int? cantidad { get; set; }
        public int? estado { get; set; }

        [ForeignKey("producto_id")]
        public ProductoFinal producto { get; set; }

        [ForeignKey("pedido_id")]
        public Pedido pedido { get; set; }
    }
}
