using System.ComponentModel.DataAnnotations.Schema;

namespace Ambrosuite.ApiService.Entities
{
    [Table("mesa")]
    public class Mesa
    {
        public int id { get; set; }
        public int? estado { get; set; }
    }
}
