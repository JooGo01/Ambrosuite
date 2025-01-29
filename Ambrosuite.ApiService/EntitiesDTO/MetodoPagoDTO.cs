namespace Ambrosuite.ApiService.EntitiesDTO
{
    public class MetodoPagoDTO
    {
        public int id { get; set; }
        public string? nombre { get; set; }
        public string? descripcion { get; set; }
        public string? url_integracion { get; set; }
        public string? codigo_proveedor { get; set; }
        public int? estado { get; set; }
    }
}
