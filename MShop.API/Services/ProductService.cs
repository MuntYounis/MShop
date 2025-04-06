using MShop.API.Controllers;
using MShop.API.data;
using MShop.API.Models;
using System.Linq;
using System.Linq.Expressions;

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

        public bool Edit(int id, Product product)
        {
            Product? productInDb = _context.Products.Find(id);
            if (productInDb == null) return false;
            _context.Products.Update(product);
            _context.SaveChanges();
            return true;

        }

        public Product? Get(Expression<Func<Product, bool>> expression)
        {
            return _context.Products.FirstOrDefault(expression);
        }

        public IEnumerable<Product> GetAll()
        {
            return _context.Products.ToList();
        }

        public bool Remove(int id)
        {
            Product? productInDB = _context.Products.Find(id);
            if (productInDB == null) return false;
            _context.Products.Remove(productInDB);
            _context.SaveChanges();
            return true;
        }
    }
}
