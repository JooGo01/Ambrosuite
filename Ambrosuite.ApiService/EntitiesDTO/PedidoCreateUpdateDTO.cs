namespace Ambrosuite.ApiService.EntitiesDTO
{
    public class PedidoCreateUpdateDTO
    {
        public double? total { get; set; }
        public int estado { get; set; }
        public int mesa_id { get; set; }
        public int usuario_id { get; set; }
    }
}
