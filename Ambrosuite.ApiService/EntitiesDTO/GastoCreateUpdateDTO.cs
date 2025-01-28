namespace Ambrosuite.ApiService.EntitiesDTO
{
    public class GastoCreateUpdateDTO
    {
        public String? observacion { get; set; }
        public DateTime fecha { get; set; }
        public int usuario_id { get; set; }
        public int estado { get; set; }
        public int categoria_gasto_id { get; set; }
        public int caja_id { get; set; }
    }
}
