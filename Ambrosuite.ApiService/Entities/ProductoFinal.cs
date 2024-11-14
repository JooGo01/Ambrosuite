using System.ComponentModel.DataAnnotations.Schema;

namespace Ambrosuite.ApiService.Entities
{
    [Table("producto_final")]
    public class ProductoFinal
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public double? precio { get; set; }
        public int? estado { get; set; }

        // Relación con Receta (uno a muchos)
        public ICollection<Receta> recetas { get; set; }
    }
}
