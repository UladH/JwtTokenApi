using App.Contracts.Models;

namespace App.Contracts.Interfaces
{
    public interface IJwtService
    {
        TokenModel CreateToken(UserForJwtModel user);
    }
}
