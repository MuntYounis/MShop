using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MShop.API.data;
using MShop.API.DTOs.Requests;
using MShop.API.DTOs.Resposnses;
using MShop.API.Models;
using MShop.API.Services;
using MShop.API.Utility;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = $"{StaticData.SuperAdmin},{StaticData.Admin},{StaticData.Company}")]
    public class ProductsController(IProductService productService) : ControllerBase
    {
        private readonly IProductService productService = productService;


        [HttpGet("")]
        [AllowAnonymous]
        public IActionResult GetAll([FromQuery] string? query, [FromQuery] int page = 1, [FromQuery] int limit = 10)
        {
            if (page <= 0 || limit <= 0) { 
                page = 1; 
                limit = 10; 
            }

            IQueryable<Product> products = productService.GetAll();

            if (!string.IsNullOrWhiteSpace(query))
            {
                products = products.Where(product =>
                    product.Name.Contains(query) ||
                    product.Description.Contains(query));
            }

            products = products.Skip((page - 1) * limit).Take(limit);

            return Ok(products.Adapt<IEnumerable<ProductResponse>>());
        }


        //[HttpGet("")]
        //public IActionResult GetAll() { 

        //    var products = productService.GetAll();

        //    if(products is null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(products.Adapt<IEnumerable<ProductResponse>>());


        //}
        [HttpGet("{id}")]
        public IActionResult GetById(int id) { 
            var product = productService.Get(e => e.Id == id);
            if (product is null) { 
                return NotFound();
            
            }
            return Ok(product.Adapt<ProductResponse>());
        
        }

        [HttpPost("")]
        public IActionResult Create([FromForm]ProductRequest productRequest)
        {

            var file = productRequest.mainImg;
            var product = productRequest.Adapt<Product>();

            if (file != null && file.Length > 0) {

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);


                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "images", fileName);


                using (var stream = System.IO.File.Create(filePath))
                {
                    file.CopyTo(stream);
                }

                product.mainImg = fileName;
                var productInDB = productService.Add(product);
               
                return CreatedAtAction(nameof(GetById),new { productInDB.Id}, productInDB);

            }
            return BadRequest();
        }

        //[HttpPut("{id}")]
        //public IActionResult Update([FromRoute] int id, [FromForm] ProductUpdateRequest productRequest) {
        //    var product = productService.Edit(id,productRequest.Adapt<Product>());
        //    if(!product) return NotFound();
        //    return NoContent();

        //}
        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromForm] ProductUpdateRequest productRequest)
        {
            var product = productRequest.Adapt<Product>();
            var success = productService.Edit(id, product, productRequest.mainImg);
            if (!success) return NotFound();
            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var productInDb = productService.Remove(id);
            if(!productInDb) return NotFound();
            return NoContent();

        }
        
    }
}
