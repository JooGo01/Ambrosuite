using System.ComponentModel.DataAnnotations.Schema;

namespace Ambrosuite.ApiService.Entities
{
    [Table("inventario")]
    public class Inventario
    {
        public int id { get; set; }
        public String? lote { get; set; }
        public double? stock { get; set; }
        public DateTime? fecha_vencimiento { get; set; }
        public int ingrediente_id { get; set; }

        [ForeignKey("ingrediente_id")]
        public Ingrediente ingrediente { get; set; }
    }
}
