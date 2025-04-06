using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MShop.API.data;
using MShop.API.Models;
using MShop.API.Services;
using Mapster;
using MShop.API.DTOs.Resposnses;
using MShop.API.DTOs.Requests;


namespace MShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController(ICategoryService categoryService) : ControllerBase
    {
        private readonly ICategoryService categoryService = categoryService;


        [HttpGet("")]
        public IActionResult getAll()
        {
            var categories = categoryService.GetAll();
            return Ok(categories.Adapt<IEnumerable<CategoryResponse>>());
        }

        [HttpGet("{id}")]
        public IActionResult getById([FromRoute]int id) { 

            var category = categoryService.Get(e=>e.Id==id);

            return category == null ? NotFound() : Ok(category.Adapt<CategoryResponse>());

        }

        [HttpPost("")]
        public IActionResult Create([FromBody]CategoryRequest categoryRequest) {

     

            var categoryInDb = categoryService.Add(categoryRequest.Adapt<Category>());
            
            //return Created($"{Request.Scheme}://{Request.Host}/api/Categories/{category.Id}",category);

            return CreatedAtAction(nameof(getById),new { categoryInDb.Id}, categoryInDb);
        
        }

        [HttpPut("{id}")]

        public IActionResult Update([FromRoute]int id,[FromBody] Category categoryRequest) {
            var categoryInDb = categoryService.Edit(id, categoryRequest.Adapt<Category>());
            if(!categoryInDb) return NotFound();

            return NoContent();

        }

        [HttpDelete("{id}")]

        public IActionResult Delete([FromRoute]int id) {
            var categoryInDb = categoryService.Remove(id);
            if (!categoryInDb) return NotFound();

            return NoContent();
        }

    }
}
