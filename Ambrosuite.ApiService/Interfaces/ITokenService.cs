using Ambrosuite.ApiService.Entities;

namespace Ambrosuite.ApiService.Interfaces
{
    public interface ITokenService { 
        string GenerateToken(Usuario usuario);
    }
}
