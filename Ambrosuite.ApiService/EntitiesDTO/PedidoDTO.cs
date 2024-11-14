namespace Ambrosuite.ApiService.EntitiesDTO
{
    public class PedidoDTO
    {
        public int id { get; set; }
        public DateTime? fecha_hora { get; set; }
        public double? total { get; set; }
        public int? estado { get; set; }
        public int mesa_id { get; set; }
        public int usuario_id { get; set; }

        public MesaDTO Mesa { get; set; }
        public UsuarioDTO Usuario { get; set; }
    }
}
