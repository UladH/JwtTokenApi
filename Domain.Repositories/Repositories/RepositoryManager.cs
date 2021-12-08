using AppDbContext;
using Domain.Contracts.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.Design;

namespace Domain.Repositories.Repositories
{
    public class RepositoryManager: IRepositoryManager
    {

        private IAppDbContext context;
        private ServiceContainer serviceContainer;
        private IServiceProvider provider;

        private bool disposed = false;

        #region constructor

        public RepositoryManager(IServiceProvider provider)
        {
            context = provider.GetService<IAppDbContext>();
            serviceContainer = new ServiceContainer();
            this.provider = provider;
        }

        #endregion

        #region public

        public IRefreshTokenRepository RefreshTokenRepository
        {
            get { return GetService<IRefreshTokenRepository>(); }
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }

                disposed = true;
            }
        }

        #endregion

        #region private

        private T GetService<T>() where T : class
        {
            var service = serviceContainer.GetService<T>();

            if (service == null)
            {
                service = provider.GetService<T>();
                serviceContainer.AddService(typeof(T), service);
            }

            return service;
        }

        #endregion
    }
}
