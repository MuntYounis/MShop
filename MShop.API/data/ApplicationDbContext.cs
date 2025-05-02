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
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Cart>().HasKey(e => new { e.ProductId, e.ApplicationUserId });
        
        }

        public DbSet<Category> Categories {  get; set; }
        public DbSet<Product> Products { get; set; }

        public DbSet<Brand> Brands { get; set; }



    }
}
