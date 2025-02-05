namespace Ambrosuite.ApiService.EntitiesDTO
{
    public class RecetaCreateUpdateDTO
    {
        public int producto_final_id { get; set; }
        public int ingrediente_id { get; set; }
        public double? cantidad { get; set; }
        public string descripcion { get; set; }
        public int? estado { get; set; }
    }
}
