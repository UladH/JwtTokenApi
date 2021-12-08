using Domain.Contracts.Interfaces.Repositories.Common;
using Domain.Contracts.Models;

namespace Domain.Contracts.Interfaces.Repositories
{
    public interface IRefreshTokenRepository: IGeneralRepository<RefreshToken>
    {
        RefreshToken GetByToken(string token);
        void DeleteExpiredTokensByUserId(string userId);
    }
}
