namespace Ambrosuite.ApiService.EntitiesDTO
{
    public class IngredienteCreateUpdateDTO
    {
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public string marca { get; set; }
        public double? alertaCantidad { get; set; }
        public int? estado { get; set; }
    }
}
