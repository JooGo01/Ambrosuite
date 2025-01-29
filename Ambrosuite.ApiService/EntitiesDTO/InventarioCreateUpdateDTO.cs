namespace Ambrosuite.ApiService.EntitiesDTO
{
    public class InventarioCreateUpdateDTO
    {
        public String? lote { get; set; }
        public double? stock { get; set; }
        public DateTime? fecha_vencimiento { get; set; }
        public int ingrediente_id { get; set; }
    }
}
