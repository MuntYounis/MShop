using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MShop.API.DTOs.Resposnses;
using MShop.API.Models;
using MShop.API.Services;
using MShop.API.Utility;
using System.Threading.Tasks;

namespace MShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = $"{StaticData.SuperAdmin}")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService) {
          
            this.userService = userService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            var users = await userService.GetAsync();
            return Ok(users.Adapt<IEnumerable<UserDto>>());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var user  = await userService.GetOneAsync(u=>u.Id ==id);
            return Ok(user.Adapt<UserDto>());   
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> ChangeRole([FromRoute]string userId,[FromQuery] string newRoleName)
        {
            var result = await userService.ChangeRole(userId,newRoleName);  
            return Ok(result);
        }

    }
}
