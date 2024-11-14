namespace Ambrosuite.ApiService.EntitiesDTO
{
    public class RecetaCreateUpdateDTO
    {
        public int productoFinalId { get; set; }
        public int ingredienteId { get; set; }
        public double? cantidad { get; set; }
        public string descripcion { get; set; }
        public int? estado { get; set; }
    }
}
