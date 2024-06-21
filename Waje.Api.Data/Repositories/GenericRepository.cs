using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Waje.Api.Data.Contract;
using Waji.Api.Data.Models;

namespace Waji.Api.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly WajeInterViewDbContext _context;
        public GenericRepository(WajeInterViewDbContext context) => _context = context;

        public async Task Create(T entity) =>await _context.Set<T>().AddAsync(entity);

        public void Delete(T entity) => _context.Set<T>().Remove(entity);

        public async Task<IQueryable<T>> GetAllAsync(bool trackChanges) =>
            !trackChanges ? _context.Set<T>().AsNoTracking() : _context.Set<T>();

        public async Task<IQueryable<T>> GetByIdAsync(Expression<Func<T, bool>> expression, bool trackChange) =>
            !trackChange ? _context.Set<T>().Where(expression).AsNoTracking() : _context.Set<T>().Where(expression);

        public void Update(T entity) => _context.Set<T>().Update(entity);
    }
}
