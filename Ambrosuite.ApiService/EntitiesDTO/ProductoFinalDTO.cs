namespace Ambrosuite.ApiService.EntitiesDTO
{
    public class ProductoFinalDTO
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public double? precio { get; set; }
        public int? estado { get; set; }

        // Podrías incluir o no las recetas, depende de la necesidad
        public ICollection<RecetaDTO> recetas { get; set; }
    }
}
