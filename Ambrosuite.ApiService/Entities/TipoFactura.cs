using System.ComponentModel.DataAnnotations.Schema;

namespace Ambrosuite.ApiService.Entities
{
    [Table("tipo_factura")]
    public class TipoFactura
    {
        public int id { get; set; }
        public string? nombre_tipo { get; set; }
        public string? descripcion { get; set; }
    }
}
