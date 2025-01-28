namespace Ambrosuite.ApiService.EntitiesDTO
{
    public class GastoDTO
    {
        public int id { get; set; }
        public String? observacion { get; set; }
        public DateTime? fecha { get; set; }
        public int usuario_id { get; set; }
        public int? estado { get; set; }
        public int categoria_gasto_id { get; set; }
        public int caja_id { get; set; }

        public CajaDTO Caja { get; set; }
        public UsuarioDTO Usuario { get; set; }
        public CategoriaGastoDTO CategoriaGasto { get; set; }
    }
}
