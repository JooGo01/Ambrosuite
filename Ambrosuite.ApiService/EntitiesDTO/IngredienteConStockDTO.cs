namespace Ambrosuite.ApiService.EntitiesDTO
{
    public class IngredienteConStockDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Marca { get; set; }
        public double? AlertaCantidad { get; set; }
        public double StockTotal { get; set; }
    }
}
