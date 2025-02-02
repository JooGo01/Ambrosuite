using System.ComponentModel.DataAnnotations.Schema;

namespace Ambrosuite.ApiService.Entities
{
    [Table("categoria")]
    public class Categoria
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public int estado { get; set; }
    }
}
