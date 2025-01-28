using System.ComponentModel.DataAnnotations.Schema;

namespace Ambrosuite.ApiService.Entities
{
    [Table("gasto")]
    public class Gasto
    {
        public int id { get; set; }
        public String? observacion { get; set; }
        public DateTime? fecha { get; set; }
        public int usuario_id { get; set; }
        public int? estado { get; set; }
        public int categoria_gasto_id { get; set; }
        public int caja_id { get; set; }


        [ForeignKey("caja_id")]
        public Caja caja { get; set; }

        [ForeignKey("usuario_id")]
        public Usuario usuario { get; set; }

        [ForeignKey("categoria_gasto_id ")]
        public CategoriaGasto categoria_gasto { get; set; }
    }
}
