using System.ComponentModel.DataAnnotations.Schema;

namespace Ambrosuite.ApiService.Entities
{
    [Table("caja")]
    public class Caja
    {
        public int id { get; set; }
        public int? numero_caja { get; set; }
    }
}
