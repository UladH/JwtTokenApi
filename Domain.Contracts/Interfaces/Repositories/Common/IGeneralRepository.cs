namespace Domain.Contracts.Interfaces.Repositories.Common
{
    public interface IGeneralRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}
