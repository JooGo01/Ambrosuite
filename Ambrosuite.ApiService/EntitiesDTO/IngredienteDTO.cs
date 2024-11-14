namespace Ambrosuite.ApiService.EntitiesDTO
{
    public class IngredienteDTO
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public string marca { get; set; }
        public double? alertaCantidad { get; set; }
        public int? estado { get; set; }

        public ICollection<RecetaDTO> recetas { get; set; }
    }
}
