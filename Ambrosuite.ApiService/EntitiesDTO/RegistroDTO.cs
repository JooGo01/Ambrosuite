namespace Ambrosuite.ApiService.EntitiesDTO
{
    public class RegistroDTO
    {
        public required string nombre { get; set; }
        public required string apellido { get; set; }
        public required DateTime fechaNacimiento { get; set; }
        public required string email { get; set; }
        public required string cuil { get; set; }
        public required string contrasenia { get; set; }
    }
}
