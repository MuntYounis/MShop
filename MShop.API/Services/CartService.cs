using Microsoft.EntityFrameworkCore;
using MShop.API.data;
using MShop.API.Models;
using MShop.API.Services;
using MShop.API.Services.IService;
using System.Linq.Expressions;

namespace MShop.API.Services
{
    public class CartService : Service<Cart>, ICartService
    {
        private readonly ApplicationDbContext _context;
        public CartService(ApplicationDbContext context):base(context) 
        {
            this._context = context;
        }


    }
}


//ApplicationDbContext _context;
//public CategoryService(ApplicationDbContext context)
//{
//    _context = context;
//}
//public async Task<Category> AddAsync(Category category, CancellationToken cancellationToken = default)
//{
//    await _context.Categories.AddAsync(category, cancellationToken);
//    await _context.SaveChangesAsync();
//    return category;
//}

//public async Task<bool> EditAsync(int id, Category category, CancellationToken cancellationToken = default)
//{
//    Category? categoryInDb = _context.Categories.Find(id);
//    if (categoryInDb == null) return false;
//    categoryInDb.Name = category.Name;
//    categoryInDb.Description = category.Description;
//    await _context.SaveChangesAsync(cancellationToken);
//    return true;
//}

//public async Task<bool> UpdateToggleAsync(int id, CancellationToken cancellationToken = default)
//{
//    Category? categoryInDb = _context.Categories.Find(id);
//    if (categoryInDb == null) return false;
//    categoryInDb.Status = !categoryInDb.Status;
//    await _context.SaveChangesAsync(cancellationToken);
//    return true;
//}

//public Category? Get(Expression<Func<Category, bool>> expression)
//{
//    return _context.Categories.FirstOrDefault(expression);
//}

//public IEnumerable<Category> GetAll()
//{
//    return _context.Categories.ToList();
//}

//public async Task<bool> RemoveAsync(int id, CancellationToken cancellationToken = default)
//{
//    Category? categoryInDb = _context.Categories.Find(id);
//    if (categoryInDb == null) return false;
//    _context.Categories.Remove(categoryInDb);
//    await _context.SaveChangesAsync(cancellationToken);
//    return true;
//}
