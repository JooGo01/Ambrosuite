using System.ComponentModel.DataAnnotations.Schema;

namespace Ambrosuite.ApiService.Entities
{
    [Table("categoria_producto")]
    public class CategoriaProducto
    {
        public int id { get; set; }
        public int categoria_id { get; set; }
        public int producto_id { get; set; }

        [ForeignKey("producto_id")]
        public ProductoFinal producto { get; set; }

        [ForeignKey("categoria_id")]
        public Categoria categoria { get; set; }
    }
}
