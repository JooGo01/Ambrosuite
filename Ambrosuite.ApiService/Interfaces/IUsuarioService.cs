using Ambrosuite.ApiService.Entities;
using Ambrosuite.ApiService.EntitiesDTO;

namespace Ambrosuite.ApiService.Interfaces
{
    public interface IUsuarioService
    {
        Task<Usuario> CrearUsuarioAsync(RegistroDTO usuario);
        Task<Usuario> LoginAsync(string email, string contrasenia);
    }
}
