using System.ComponentModel.DataAnnotations.Schema;

namespace Ambrosuite.ApiService.Entities
{
    [Table("facturacion_detalle")]
    public class FacturacionDetalle
    {
        public int id { get; set; }
        public int producto_id { get; set; }
        public int facturacion_id { get; set; }
        public string tipo_unidad { get; set; }
        public int unidad { get; set; }

        [ForeignKey("producto_id")]
        public ProductoFinal producto { get; set; }

        [ForeignKey("facturacion_id")]
        public Facturacion facturacion { get; set; }
    }
}
