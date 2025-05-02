using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MShop.API.Models;
using MShop.API.Services;
using System.Threading.Tasks;

namespace MShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartsController : ControllerBase
    {
        private readonly ICartService cartService;
        private readonly UserManager<ApplicationUser> userManager;

        public CartsController(ICartService cartService, UserManager<ApplicationUser> userManager) {

            this.cartService = cartService;
            this.userManager = userManager;
        }
        [HttpPost("{ProductId}")]
        public async Task<IActionResult> AddToCart([FromRoute]int ProductId,[FromQuery] int count)
        {
            var appUser = userManager.GetUserId(User);
            var cart = new Cart()
            {
                ProductId = ProductId,
                Count = count,
                ApplicationUserId = appUser
            };
            await cartService.AddAsync(cart);
            return Ok(cart);
        }
    }
}
