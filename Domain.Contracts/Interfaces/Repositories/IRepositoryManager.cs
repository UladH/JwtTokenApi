namespace Domain.Contracts.Interfaces.Repositories
{
    public interface IRepositoryManager: IDisposable
    {
        IRefreshTokenRepository RefreshTokenRepository { get; }

        void SaveChanges();
    }
}
