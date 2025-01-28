using System.ComponentModel.DataAnnotations.Schema;

namespace Ambrosuite.ApiService.Entities
{
    [Table("caja_pedido")]
    public class CajaPedido
    {
        public int id { get; set; }
        public int caja_id { get; set; }
        public int pedido_id { get; set; }
        public DateTime fecha_hora { get; set; }
        [ForeignKey("caja_id")]
        public Caja caja { get; set; }
        [ForeignKey("pedido_id")]
        public Pedido pedido { get; set; }
    }
}
