namespace Ambrosuite.ApiService.EntitiesDTO
{
    public class PedidoDetalleCreateUpdateDTO
    {
        public int producto_id { get; set; }
        public int pedido_id { get; set; }
        public int? cantidad { get; set; }
        public int? estado { get; set; }
    }
}
