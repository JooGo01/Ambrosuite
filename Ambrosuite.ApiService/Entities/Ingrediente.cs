using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Ambrosuite.ApiService.Entities
{
    [Table("ingrediente")]
    public class Ingrediente
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public string marca { get; set; }
        [Column("alerta_cantidad")]
        public double? alertaCantidad { get; set; }
        public int? estado { get; set; }

        // Relación con Receta (uno a muchos)
        [JsonIgnore]
        public ICollection<Receta> recetas { get; set; }
    }
}
