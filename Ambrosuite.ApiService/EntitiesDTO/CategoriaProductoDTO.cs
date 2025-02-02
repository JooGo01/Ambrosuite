namespace Ambrosuite.ApiService.EntitiesDTO
{
    public class CategoriaProductoDTO
    {
        public int id { get; set; }
        public int producto_id { get; set; }
        public int categoria_id { get; set; }

        public ProductoFinalDTO Producto { get; set; }
        public CategoriaDTO Categoria { get; set; }
    }
}
