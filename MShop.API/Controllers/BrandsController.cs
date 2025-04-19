using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MShop.API.data;
using MShop.API.DTOs.Resposnses;
using MShop.API.Models;
using Mapster;
using MShop.API.DTOs.Requests;
using MShop.API.DTOs.Resposnses;
using Azure.Core;
using MShop.API.Services;
using Microsoft.OpenApi.Models;
namespace MShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController(IBrandService brandService): ControllerBase
    {
        private readonly IBrandService brandService = brandService;


        [HttpGet("")]
        public IActionResult getAll()
        {
            var brands = brandService.GetAll();
            return Ok(brands.Adapt<IEnumerable<BrandResponse>>());
        }

        [HttpGet("{id}")]
        public IActionResult getById([FromRoute] int id)
        { 
            var brand = brandService.Get(b=>b.Id == id);
            return brand ==null? NotFound() : Ok(brand.Adapt<BrandResponse>());
  
        }
        [HttpPost("")]
        public IActionResult Create([FromBody] BrandRequest brandRequest)
        {
            var brandInDb = brandService.Add(brandRequest.Adapt<Brand>());
            return CreatedAtAction(nameof(getById),new { brandInDb.Id },brandInDb); 

        }
        [HttpPut("{id}")]
        public IActionResult Update([FromRoute]int id,[FromBody] Brand brandRequest)
        {
            var brandInDb = brandService.Edit(id, brandRequest.Adapt<Brand>());
            if(!brandInDb) return NotFound();
            return NoContent(); 
        }
        [HttpDelete]
        public IActionResult Delete([FromRoute]int id)
        {
            var brandInDb= brandService.Remove(id);
            if(!brandInDb) return NotFound();
            return NoContent();
        }
    }
}
