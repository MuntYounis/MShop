
using Microsoft.EntityFrameworkCore;
using MShop.API.data;
using MShop.API.Models;
using System.Linq;
using System.Linq.Expressions;

namespace MShop.API.Services.IService
{
    public class Service<T> : IService<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbset;

        public Service(ApplicationDbContext context)
        {
            this._context = context;
            _dbset = _context.Set<T>(); 
        }
        public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await _context.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync();
            return entity;
        }
        
        public async Task<T?> GetOneAsync(Expression<Func<T, bool>> expression = null, Expression<Func<T, object>>[] includes = null
            , bool isTracked = true)
        {
            var all = await GetAsync(expression, includes, isTracked);
            return all.FirstOrDefault();
        }



        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>>? expression = null, Expression<Func<T, object>>?[] includes = null
            ,bool isTracked = true)
        {
            IQueryable<T> entities = _dbset;
            if (expression is not null) {
                entities = entities.Where(expression);
                }
            if (includes is not null) {
                foreach (var item in includes) {
                    entities = entities.Include(item);
                }
            }
            if (!isTracked) {
                entities = entities.AsNoTracking();
            }
            return await entities.ToListAsync();
        }

        public async Task<bool> RemoveAsync(int id, CancellationToken cancellationToken = default)
        {
            T? entityInDb = _dbset.Find(id);
            if (entityInDb == null) return false;
            _dbset.Remove(entityInDb);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }


    }
}
