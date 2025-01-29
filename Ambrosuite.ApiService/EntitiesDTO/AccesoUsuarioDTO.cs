namespace Ambrosuite.ApiService.EntitiesDTO
{
    public class AccesoUsuarioDTO
    {
        public int id { get; set; }
        public DateTime? fecha_hora_acceso { get; set; }
        public int? usuario_id { get; set; }

        public UsuarioDTO usuario { get; set; }
    }
}
