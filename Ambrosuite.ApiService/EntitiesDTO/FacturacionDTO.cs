namespace Ambrosuite.ApiService.EntitiesDTO
{
    public class FacturacionDTO
    {
        public int id { get; set; }
        public DateTime? fecha_emision { get; set; }
        public int? punto_venta { get; set; }
        public int? numero_factura { get; set; }
        public double? total { get; set; }
        public int? estado { get; set; }
        public int? metodo_pago_id { get; set; }
        public int? tipo_factura_id { get; set; }

        public MetodoPagoDTO MetodoPago { get; set; }
        public TipoFacturaDTO TipoFactura { get; set; }
    }
}
