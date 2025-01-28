using System.ComponentModel.DataAnnotations.Schema;

namespace Ambrosuite.ApiService.Entities
{
    [Table("caja_pedido")]
    public class CajaMovimiento
    {
        public int id { get; set; }
        public int? movimiento { get; set; }
        public DateTime? fecha_hora_movimiento { get; set; }
        public int usuario_id { get; set; }
        public int caja_id { get; set; }


        [ForeignKey("caja_id")]
        public Caja caja { get; set; }

        [ForeignKey("usuario_id")]
        public Usuario usuario { get; set; }
    }
}
