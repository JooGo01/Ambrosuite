using System.ComponentModel.DataAnnotations.Schema;

namespace Ambrosuite.ApiService.Entities
{
    [Table("categoria_gasto")]
    public class CategoriaGasto
    {
        public int id { get; set; }
        public string gasto_nombre { get; set; }
        public string gasto_descripcion { get; set; }
    }
}
