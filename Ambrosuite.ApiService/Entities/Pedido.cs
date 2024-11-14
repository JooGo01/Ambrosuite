using System.ComponentModel.DataAnnotations.Schema;

namespace Ambrosuite.ApiService.Entities
{
    [Table("pedido")]
    public class Pedido
    {
        public int id { get; set; }
        public DateTime? fecha_hora { get; set; }
        public double? total { get; set; }
        public int? estado { get; set; }
        public int mesa_id { get; set; }
        public int usuario_id { get; set; }

        [ForeignKey("mesa_id")]
        public Mesa mesa { get; set; }

        [ForeignKey("usuario_id")]
        public Usuario usuario { get; set; }
    }
}
