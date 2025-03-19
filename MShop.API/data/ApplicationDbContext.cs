using Microsoft.EntityFrameworkCore;
using MShop.API.Models;

namespace MShop.API.data

{

        public class ApplicationDbContext : DbContext
        {
            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                : base(options)
            {
            }
        

        public DbSet<Category> Categories {  get; set; }
    }
}
