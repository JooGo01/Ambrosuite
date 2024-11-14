using System.ComponentModel.DataAnnotations.Schema;

namespace Ambrosuite.ApiService.Entities
{
    [Table("receta")]
    public class Receta
    {
        public int id { get; set; }
        [Column("producto_final_id")]
        public int productoFinalId { get; set; }
        [Column("ingrediente_id")]
        public int ingredienteId { get; set; }
        public double? cantidad { get; set; }
        public string descripcion { get; set; }
        public int? estado { get; set; }

        // Relaciones
        public ProductoFinal productoFinal { get; set; }
        public Ingrediente ingrediente { get; set; }
    }
}
