using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Ambrosuite.ApiService.Entities
{
    [Table("usuario")]
    public class Usuario
    {
        public int id { get; set; }
        public required String nombre { get; set; }
        public required String apellido { get; set; }
        public required DateTime fecha_nacimiento { get; set; }
        public required String email { get; set; }
        public required String cuil { get; set; }
        public required String contrasenia { get; set; }

        public int baja { get; set; }
        public int rol_id { get; set; }

        [JsonIgnore]
        public Rol Rol { get; set; }
    }
}
