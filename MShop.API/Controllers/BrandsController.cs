using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MShop.API.data;
using MShop.API.DTOs.Resposnses;
using MShop.API.Models;
using Mapster;
using MShop.API.DTOs.Requests;
using MShop.API.DTOs.Resposnses;
using Azure.Core;
namespace MShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        ApplicationDbContext _context;
        public BrandsController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet("")]
        public IActionResult getAll()
        {
            var brands = _context.Brands.ToList();
            if (brands is null) {
                return NotFound();
            }
            return Ok(brands);
        }

        [HttpGet("{id}")]
        public IActionResult getById([FromRoute] int id)
        { 
            var brand = _context.Brands.Find(id);
            if (brand is null)
            {
                return NotFound();
            }
            return Ok(brand);
        
        }
        [HttpPost("")]
        public IActionResult Create([FromBody] BrandRequest brandRequest)
        {
            var brand = brandRequest.Adapt<Brand>();
            _context.Brands.Add(brand);
            _context.SaveChanges();
            var response = brand.Adapt<BrandResponse>();
            return Created($"https://localhost:7245/api/Brands/{brand.Id}", response);

        }
    }
}
