using System.ComponentModel.DataAnnotations;

namespace Ambrosuite.Web.Entities
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "El CUIL es obligatorio")]
        public string email { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [DataType(DataType.Password)]
        public string password { get; set; }
    }
}