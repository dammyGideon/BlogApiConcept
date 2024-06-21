using Microsoft.EntityFrameworkCore;
using Waje.Api.Data.Contract;
using Waji.Api.Data.Models;
using Waje.Api.Data.Repositories;
using Waji.Api.Data.Repositories;

namespace Waje.Api.Data.UnitOfWork
{
    public class UnitOfWorkService : IUnitOfWork

    {
        private readonly WajeInterViewDbContext _context;
        private Dictionary<Type, object> _repositories;

        public UnitOfWorkService(WajeInterViewDbContext context)
        {
            _context = context;
            _repositories = new Dictionary<Type, object>();
        }

        public IGenericRepository<T> GetRepository<T>() where T : class
        {
            if (!_repositories.TryGetValue(typeof(T), out object repository))
            {
                repository = new GenericRepository<T>(_context);
                _repositories.Add(typeof(T), repository);
            }
            return (IGenericRepository<T>)repository;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
