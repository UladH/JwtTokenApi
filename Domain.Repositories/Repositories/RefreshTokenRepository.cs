using AppDbContext;
using Domain.Contracts.Interfaces.Repositories;
using Domain.Contracts.Models;
using Domain.Repositories.Repositories.Common;

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
    }
}
