namespace Ambrosuite.ApiService.EntitiesDTO
{
    public class FacturacionDetalleCreateUpdateDTO
    {
        public int producto_id { get; set; }
        public int facturacion_id { get; set; }
        public string? tipo_unidad { get; set; }
        public int? unidad { get; set; }
    }
}
