using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MShop.API.data;
using MShop.API.DTOs.Requests;
using MShop.API.DTOs.Resposnses;
using MShop.API.Models;
using MShop.API.Services;
using System.Runtime.CompilerServices;

namespace MShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IProductService productService) : ControllerBase
    {
        private readonly IProductService productService = productService;


        [HttpGet("")]
        public IActionResult GetAll() { 
            
            var products = productService.GetAll();

            if(products is null)
            {
                return NotFound();
            }
            return Ok(products.Adapt<IEnumerable<ProductResponse>>());


        }
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

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var productInDb = productService.Remove(id);
            if(!productInDb) return NotFound();
            return NoContent();

        }
        
    }
}
