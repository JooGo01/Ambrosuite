namespace Ambrosuite.ApiService.EntitiesDTO
{
    public class UsuarioCreateUpdateDTO
    {
        public string nombre { get; set; }
        public string apellido { get; set; }
        public DateTime fecha_nacimiento { get; set; }
        public string email { get; set; }
        public string cuil { get; set; }
        public string contrasenia { get; set; }
        public int baja { get; set; }

        // Solo necesitas el rol_id
        public int rol_id { get; set; }
    }
}
