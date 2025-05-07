namespace Ambrosuite.ApiService.EntitiesDTO
{
    public class PedidoDetalleDTO
    {
        public int id { get; set; }
        public int producto_id { get; set; }
        public int pedido_id { get; set; }
        public int? cantidad { get; set; }
        public int? estado { get; set; }

        public string nombreProducto { get; set; }

        public ProductoFinalDTO Producto { get; set; }  
        public PedidoDTO Pedido { get; set; }
    }
}
