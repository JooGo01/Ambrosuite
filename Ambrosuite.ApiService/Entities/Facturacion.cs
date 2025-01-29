using System.ComponentModel.DataAnnotations.Schema;

namespace Ambrosuite.ApiService.Entities
{
    [Table("facturacion")]
    public class Facturacion
    {
        public int id { get; set; }
        public DateTime? fecha_emision { get; set; }
        public int? punto_venta { get; set; }
        public int? numero_factura { get; set; }
        public double? total{ get; set; }
        public int? estado { get; set; }    
        public int? metodo_pago_id { get; set; }
        public int? tipo_factura_id { get; set; }

        [ForeignKey("metodo_pago_id")]
        public MetodoPago metodoPago { get; set; }
        [ForeignKey("tipo_factura_id")]
        public TipoFactura tipoFactura { get; set; }
    }
}
