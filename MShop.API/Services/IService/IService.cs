using MShop.API.Models;
using System.Linq.Expressions;

namespace MShop.API.Services.IService
{
    public interface IService<T> where T : class
    {
        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> ?expression=null,Expression<Func<T, object>>[] includes = null, bool isTracked=true);

        Task<T?> GetOneAsync(Expression<Func<T, bool>> expression,Expression<Func<T,object>>[]includes = null, bool isTracked = true);

        Task<T> AddAsync(T category, CancellationToken cancellationToken = default);

        Task<bool> RemoveAsync(int id, CancellationToken cancellationToken = default);
    }
}
