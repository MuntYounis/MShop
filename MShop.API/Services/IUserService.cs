using MShop.API.Models;
using MShop.API.Services.IService;
using System.Linq.Expressions;

namespace MShop.API.Services
{
    public interface IUserService : IService<ApplicationUser>
    {

        Task<bool> ChangeRole(string userId, string roleName);




    }
}
