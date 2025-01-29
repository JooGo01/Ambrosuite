namespace Ambrosuite.ApiService.EntitiesDTO
{
    public class InventarioDTO
    {
        public int id { get; set; }
        public String? lote { get; set; }
        public double? stock { get; set; }
        public DateTime? fecha_vencimiento { get; set; }
        public int ingrediente_id { get; set; }

        public IngredienteDTO Ingrediente { get; set; }
    }
}
