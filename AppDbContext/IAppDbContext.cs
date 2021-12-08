using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Diagnostics.CodeAnalysis;

namespace AppDbContext
{
    public interface IAppDbContext: IInfrastructure<IServiceProvider>, IResettableService, IDisposable, IAsyncDisposable
    {
        #region dbContext interface description

        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        EntityEntry<TEntity> Entry<TEntity>([NotNullAttribute] TEntity entity) where TEntity : class;
        int SaveChanges();

        #endregion
    }
}
