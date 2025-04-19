using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MShop.API.data;
using MShop.API.Models;
using MShop.API.Services;
using Mapster;
using MShop.API.DTOs.Resposnses;
using MShop.API.DTOs.Requests;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;


namespace MShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] //for anyone to use this controller they should have cookies
    public class CategoriesController(ICategoryService categoryService) : ControllerBase
    {
        private readonly ICategoryService categoryService = categoryService;


        [HttpGet("")]
        public async Task<IActionResult> getAll()
        {
            var categories =await categoryService.GetAsync();
            return Ok(categories.Adapt<IEnumerable<CategoryResponse>>());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> getById([FromRoute]int id) { 

            var category =await categoryService.GetOneAsync(e=>e.Id==id);

            return category == null ? NotFound() : Ok(category.Adapt<CategoryResponse>());

        }

        [HttpPost("")]
        public async Task<IActionResult> CreateAsync([FromBody]CategoryRequest categoryRequest, CancellationToken cancellationToken) {

     

            var categoryInDb =await categoryService.AddAsync(categoryRequest.Adapt<Category>(),cancellationToken);
            
            //return Created($"{Request.Scheme}://{Request.Host}/api/Categories/{category.Id}",category);

            return CreatedAtAction(nameof(getById),new { categoryInDb.Id}, categoryInDb);
        
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateAsync([FromRoute]int id,[FromBody] Category categoryRequest) {
            var categoryInDb =await categoryService.EditAsync(id, categoryRequest.Adapt<Category>());
            if(!categoryInDb) return NotFound();

            return NoContent();

        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete([FromRoute]int id) {
            var categoryInDb =await categoryService.RemoveAsync(id);
            if (!categoryInDb) return NotFound();

            return NoContent();
        }

    }
}
