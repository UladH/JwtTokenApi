using AppDbContext;
using Domain.Contracts.Interfaces.Repositories;
using Domain.Contracts.Models;
using Domain.Repositories.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repositories.Repositories
{
    public class RefreshTokenRepository: GeneralRepository<RefreshToken>, IRefreshTokenRepository
    {
        #region constructor

        public RefreshTokenRepository(IAppDbContext context)
            :base(context)
        {

        }

        #endregion

        #region public

        public RefreshToken GetByToken(string token)
        {
            return dbset.Where(item => item.Token == token)
                .Include(item => item.User)
                .FirstOrDefault();
        }

        public void DeleteExpiredTokensByUserId(string userId)
        {
            var currenDate = DateTimeOffset.UtcNow;
            var expiredTokens = dbset.Where(item => item.User.Id == userId 
                && item.Expiration < currenDate);
            dbset.RemoveRange(expiredTokens);
        }

        #endregion
    }
}
