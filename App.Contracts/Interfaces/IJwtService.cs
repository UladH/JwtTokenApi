using App.Contracts.Models;

namespace App.Contracts.Interfaces
{
    public interface IJwtService
    {
        Task<TokenPairModel> CreateTokenPair(UserForJwtModel userForJwt);
    }
}
