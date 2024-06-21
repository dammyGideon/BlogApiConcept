using System.Linq.Expressions;

namespace Waje.Api.Data.Contract
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IQueryable<T>> GetAllAsync(bool trackchanges);
        Task<IQueryable<T>> GetByIdAsync(Expression<Func<T, bool>> expression, bool trackChange);
        Task Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
