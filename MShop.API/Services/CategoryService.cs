using Microsoft.EntityFrameworkCore;
using MShop.API.data;
using MShop.API.Models;
using System.Linq.Expressions;

namespace MShop.API.Services
{
    public class CategoryService : ICategoryService
    {
        ApplicationDbContext _context;
        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }
        public Category Add(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
            return category;
        }

        public bool Edit(int id, Category category)
        {
            Category? categoryInDb = _context.Categories.AsNoTracking().FirstOrDefault(c=>c.Id == id);
            if (categoryInDb == null) return false;
            category.Id = id;
            _context.Categories.Update(category);
            _context.SaveChanges();
            return true;
        }

        public Category? Get(Expression<Func<Category, bool>> expression)
        {
            return _context.Categories.FirstOrDefault(expression);
        }

        public List<Category> GetAll()
        {
            return _context.Categories.ToList();
        }

        public bool Remove(int id)
        {
            Category? categoryInDb = _context.Categories.Find(id);
            if (categoryInDb == null) return false;
            _context.Categories.Remove(categoryInDb);
            _context.SaveChanges();
            return true;
        }

        IEnumerable<Category> ICategoryService.GetAll()
        {
            return GetAll();
        }
    }
}
