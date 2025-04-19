using MShop.API.Models;
using MShop.API.Services.IService;
using System.Linq.Expressions;

namespace MShop.API.Services
{
    public interface ICategoryService : IService<Category>
    {


        Task<bool> EditAsync(int id, Category category, CancellationToken cancellationToken = default);

        Task<bool> UpdateToggleAsync(int id, CancellationToken cancellationToken = default);

        


    }
}
