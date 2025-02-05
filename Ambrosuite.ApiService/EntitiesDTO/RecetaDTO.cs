namespace Ambrosuite.ApiService.EntitiesDTO
{
    public class RecetaDTO
    {
        public int id { get; set; }
        public int producto_final_id { get; set; }
        public int ingrediente_id { get; set; }
        public double? cantidad { get; set; }
        public string descripcion { get; set; }
        public int? estado { get; set; }

        // Simplificación de relaciones
        public ProductoFinalDTO producto_final { get; set; }
        public IngredienteDTO ingrediente { get; set; }
    }
}
