namespace Ambrosuite.ApiService.EntitiesDTO
{
    public class CajaPedidoDTO
    {
        public int id { get; set; }
        public int pedido_id { get; set; }
        public int caja_id { get; set; }
        public DateTime? fecha_hora { get; set; }

        public CajaDTO Caja { get; set; }
        public PedidoDTO Pedido { get; set; }
    }
}
