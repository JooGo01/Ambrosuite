using System.ComponentModel.DataAnnotations;

namespace Ambrosuite.Web.Entities
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "El CUIL es obligatorio")]
        public string CUIL { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}