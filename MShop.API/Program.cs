
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MShop.API.data;
using MShop.API.Models;
using MShop.API.Services;
using Scalar.AspNetCore;

namespace MShop.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            //builder.Services.AddScoped<IOS,MacServices>();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection")));

            builder.Services.AddScoped<ICategoryService,CategoryService>();

            builder.Services.AddScoped<IProductService,ProductService>();

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = false;
            }
                ).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();

            }

            app.UseHttpsRedirection();

            app.UseAuthorization();



            //using (var scope = app.Services.CreateScope())
            //{
            //    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            //    try
            //    {
            //        context.Database.CanConnect();
            //        Console.WriteLine("done");
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine("error");
            //    }
            //}






            //var context = new ApplicationDbContext();
            //try
            //{
            //    context.Database.CanConnect();
            //    Console.WriteLine("done");
            //}
            //catch (Exception ex) {
            //    Console.WriteLine("error");
            //}

            app.MapControllers();

            app.Run();
        }
    }
}
