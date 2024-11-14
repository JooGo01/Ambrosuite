namespace Ambrosuite.ApiService.EntitiesDTO
{
    public class RecetaDTO
    {
        public int id { get; set; }
        public int productoFinalId { get; set; }
        public int ingredienteId { get; set; }
        public double? cantidad { get; set; }
        public string descripcion { get; set; }
        public int? estado { get; set; }

        // Simplificación de relaciones
        public ProductoFinalDTO productoFinal { get; set; }
        public IngredienteDTO ingrediente { get; set; }
    }
}
