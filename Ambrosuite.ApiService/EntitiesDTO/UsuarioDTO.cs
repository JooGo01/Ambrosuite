namespace Ambrosuite.ApiService.EntitiesDTO
{
    public class UsuarioDTO
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public DateTime fecha_nacimiento { get; set; }
        public string email { get; set; }
        public string cuil { get; set; }
        public string contrasenia { get; set; }
        public int baja { get; set; }

        // Foreign Key para Rol
        public int rol_id { get; set; }

        // Incluye los detalles del Rol
        public RolDTO Rol { get; set; }
    }
}
