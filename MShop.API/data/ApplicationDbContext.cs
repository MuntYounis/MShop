using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MShop.API.Models;

namespace MShop.API.data

{

        public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
        {
        internal object brands;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                : base(options)
            {
            }
        

        public DbSet<Category> Categories {  get; set; }
        public DbSet<Product> Products { get; set; }

        public DbSet<Brand> Brands { get; set; }



    }
}
