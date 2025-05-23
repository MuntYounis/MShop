﻿using MShop.API.Models;
using System.Linq.Expressions;


namespace MShop.API.Services

{
    public interface IProductService
    {
        IQueryable<Product>GetAll();
        Product ? Get(Expression<Func<Product, bool>> expression);
        Product Add(Product product);

        bool Edit(int id, Product product, IFormFile mainImg);

        bool Remove(int id);
    }
}
