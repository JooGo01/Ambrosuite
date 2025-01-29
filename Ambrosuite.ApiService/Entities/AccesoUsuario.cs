using System.ComponentModel.DataAnnotations.Schema;

namespace Ambrosuite.ApiService.Entities
{
    [Table("acceso_usuario")]
    public class AccesoUsuario
    {
        public int id { get; set; }
        public DateTime? fecha_hora_acceso { get; set; }
        public int? usuario_id { get; set; }
        [ForeignKey("usuario_id")]
        public Usuario usuario { get; set; }
    }
}
