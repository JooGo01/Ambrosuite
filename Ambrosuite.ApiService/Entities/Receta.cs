using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ambrosuite.ApiService.Entities
{
    [Table("receta")]
    public class Receta
    {
        [Key]
        public int id { get; set; }

        [Column("producto_final_id")]
        public int producto_final_id { get; set; }

        [Column("ingrediente_id")]
        public int ingrediente_id { get; set; }

        [Column("cantidad")]
        public double? cantidad { get; set; }

        [Column("descripcion")]
        public string? descripcion { get; set; }

        [Column("estado")]
        public int? estado { get; set; }

        // Relaciones
        [ForeignKey(nameof(producto_final_id))]
        public ProductoFinal producto_final { get; set; }

        [ForeignKey(nameof(ingrediente_id))]
        public Ingrediente ingrediente { get; set; }
    }

}
