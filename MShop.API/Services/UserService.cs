using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MShop.API.data;
using MShop.API.Models;
using MShop.API.Services;
using MShop.API.Services.IService;
using System.Linq.Expressions;

namespace MShop.API.Services
{
    public class UserService : Service<ApplicationUser>, IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(ApplicationDbContext context, UserManager<ApplicationUser> userManager) :base(context) 
        {
            this._context = context;
            this._userManager = userManager;
        }
       
        public async Task<bool> ChangeRole(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is not null) { 
                var oldRoles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, oldRoles);

                var result = await _userManager.AddToRoleAsync(user, roleName);
                if (result.Succeeded)
                {
                    return true;
                }
                else
                {
                    return false;
                
                }
                
            }
            return false;
        }
    }
}


