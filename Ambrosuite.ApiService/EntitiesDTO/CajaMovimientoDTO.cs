namespace Ambrosuite.ApiService.EntitiesDTO
{
    public class CajaMovimientoDTO
    {
        public int id { get; set; }
        public int? movimiento { get; set; }
        public DateTime? fecha_hora_movimiento { get; set; }
        public int usuario_id { get; set; }
        public int caja_id { get; set; }

        public UsuarioDTO Usuario { get; set; }
        public CajaDTO Caja { get; set; }
    }
}
