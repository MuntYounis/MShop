using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MShop.API.data;
using MShop.API.Models;

namespace MShop.API.Utility.DBInitializer
{
    public class DBInitializer : IDBInitializer
    {
        private readonly ApplicationDbContext context;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public DBInitializer(ApplicationDbContext context, RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }
        public async Task Initialize()
        {
            try
            {
                if (context.Database.GetPendingMigrations().Any())
                    context.Database.Migrate();
            }
            catch (Exception ex) { 
                Console.WriteLine(ex.Message);
            }

            if (roleManager.Roles is not null)
            {

                await roleManager.CreateAsync(new(StaticData.SuperAdmin));
                await roleManager.CreateAsync(new(StaticData.Admin));
                await roleManager.CreateAsync(new(StaticData.Customer));
                await roleManager.CreateAsync(new(StaticData.Company));
                await userManager.CreateAsync(new()
                {
                    FirstName = "super",
                    LastName = "admin",
                    Gender = ApplicationUserGender.Male,
                    BirthOfDate = new DateTime(1994, 9, 9),
                    Email = "admin@mshop.com",
                }, "Admin@1");

                var user = await userManager.FindByEmailAsync("admin@mshop.com");

                await userManager.AddToRoleAsync(user, StaticData.SuperAdmin);

            }



            
        }

    }
}
