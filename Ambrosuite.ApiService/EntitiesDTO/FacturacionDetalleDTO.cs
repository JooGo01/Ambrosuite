namespace Ambrosuite.ApiService.EntitiesDTO
{
    public class FacturacionDetalleDTO
    {
        public int id { get; set; }
        public int producto_id { get; set; }
        public int facturacion_id { get; set; }
        public string? tipo_unidad { get; set; }
        public int? unidad { get; set; }

        public ProductoFinalDTO Producto { get; set; }
        public FacturacionDTO Facturacion { get; set; }
    }
}
