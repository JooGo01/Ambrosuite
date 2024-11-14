using System.ComponentModel.DataAnnotations.Schema;

namespace Ambrosuite.ApiService.Entities
{
    [Table("rol")]
    public class Rol
    {
        public int id { get; set; }
        public String nombre_rol { get; set; }
        public String descripcion_rol { get; set; }

        public ICollection<Usuario> Usuarios { get; set; }
    }
}
