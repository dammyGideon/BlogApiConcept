using Waje.Api.Data.Contract;

namespace Waje.Api.Data.UnitOfWork

{
    public interface IUnitOfWork
    {
        IGenericRepository<T> GetRepository<T>() where T : class;
        Task SaveChangesAsync();

    }
}
