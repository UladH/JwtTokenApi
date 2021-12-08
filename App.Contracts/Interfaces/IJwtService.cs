using App.Contracts.Models;

namespace App.Contracts.Interfaces
{
    public interface IJwtService
    {
        Task<TokenPairModel> CreateTokenPair(UserForJwtModel userForJwt);
        TokenPairModel CreateTokenPairByRefreshToken(RefreshTokenModel refreshTokenModel);
    }
}
