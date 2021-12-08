using AppDbContext;
using Domain.Contracts.Interfaces.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repositories.Repositories.Common
{
    public abstract class GeneralRepository<T> : IGeneralRepository<T> where T : class
    {
        protected IAppDbContext context;
        protected readonly DbSet<T> dbset;

        #region constructor

        public GeneralRepository(IAppDbContext context)
        {
            this.context = context;
            dbset = context.Set<T>();
        }

        #endregion

        #region pubic

        public virtual IEnumerable<T> GetAll()
        {
            return dbset.AsEnumerable();
        }

        public virtual void Add(T entity)
        {
            dbset.Add(entity);
        }

        public virtual void Delete(T entity)
        {
            dbset.Remove(entity);
        }

        public virtual void Update(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }

        #endregion
    }
}
