using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MShop.API.Controllers;
using MShop.API.data;
using MShop.API.Models;
using System.Linq;
using System.Linq.Expressions;
using MShop.API.DTOs.Requests;
using MShop.API.DTOs.Resposnses;
using MShop.API.Services;



namespace MShop.API.Services
{
    public class ProductService : IProductService
    {
        ApplicationDbContext _context;
        public ProductService(ApplicationDbContext context)
        {
            _context = context; 
        }
        public Product Add(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return product;
        }



        public Product? Get(Expression<Func<Product, bool>> expression)
        {
            return _context.Products.FirstOrDefault(expression);
        }

        public IQueryable<Product> GetAll()
        {
            return _context.Products;
        }

        //public bool Edit(int id, Product product)
        //{
        //    var productInDb = _context.Products.AsNoTracking().FirstOrDefault(product => product.Id==id);
        //    var file = product.mainImg;

        //    if (productInDb != null) {
        //        if (file != null && file.Length > 0)
        //        {
        //            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
        //        }
        //        product.Id = id;
        //        product.mainImg = productInDb.mainImg;
        //        _context.Products.Update(product);
        //        _context.SaveChanges();
        //        return true;

        //    }
        //    return false;
        //}
        public bool Edit(int id, Product product, IFormFile? mainImg)
        {
            var productInDb = _context.Products.AsNoTracking().FirstOrDefault(p => p.Id == id);
            if (productInDb == null) return false;

            // If there's a new file, save it
            if (mainImg != null && mainImg.Length > 0)
            {
                // Delete the old image
                var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "images", productInDb.mainImg);
                if (System.IO.File.Exists(oldPath))
                    System.IO.File.Delete(oldPath);

                // Save new image
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(mainImg.FileName);
                var newPath = Path.Combine(Directory.GetCurrentDirectory(), "images", fileName);
                using (var stream = System.IO.File.Create(newPath))
                {
                    mainImg.CopyTo(stream);
                }

                product.mainImg = fileName; // set new filename
            }
            else
            {
                product.mainImg = productInDb.mainImg; // keep old image if no new one
            }

            product.Id = id;
            _context.Products.Update(product);
            _context.SaveChanges();
            return true;
        }

        public bool Remove(int id)//removed from [fromroute]
        {
            Product? productInDB = _context.Products.Find(id);
            if (productInDB == null) return false;
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "images", productInDB.mainImg);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            _context.Products.Remove(productInDB);
            _context.SaveChanges();
            return true;
        }
    }
}
