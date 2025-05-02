using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MShop.API.DTOs.Requests;
using MShop.API.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using MShop.API.Utility;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> signInManager;
        private readonly IEmailSender emailSender;
        private readonly RoleManager<IdentityRole> roleManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.emailSender = emailSender;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]RegisterRequest registerRequest) {
            
            var applicationUser = registerRequest.Adapt<ApplicationUser>();

            var result = await userManager.CreateAsync(applicationUser, registerRequest.Password);

            if (result.Succeeded) {



                await emailSender.SendEmailAsync(applicationUser.Email, "Welcome", $"<h1>Hello.. {applicationUser.Email} </h1>");
                await userManager.AddToRoleAsync(applicationUser, StaticData.Customer);
                await signInManager.SignInAsync(applicationUser, false);



                return NoContent();
            }
            return BadRequest(result.Errors);


        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest) { 
           var applicationUser =  await userManager.FindByEmailAsync(loginRequest.Email);
            if(applicationUser != null)
            {
              var result = await userManager.CheckPasswordAsync(applicationUser, loginRequest.Password);

                List<Claim> claims = new();
                claims.Add(new(ClaimTypes.Name, applicationUser.UserName));
                claims.Add(new("id",applicationUser.Id));
                var userRoles = await userManager.GetRolesAsync(applicationUser);

                if (userRoles.Count > 0) {
                    foreach (var item in userRoles) {
                        claims.Add(new(ClaimTypes.Role, item));
                    }

                }

                if (result)
                {
                    SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("emZeMa0CDznplILmIN9pWcQQRhUqMwz3"));
                    SigningCredentials signingCredentials = new SigningCredentials(symmetricSecurityKey,SecurityAlgorithms.HmacSha256);


                    var jwtToken = new JwtSecurityToken(
                        claims:claims,
                        expires:DateTime.Now.AddMinutes(30),
                        signingCredentials: signingCredentials

                        );

                    string token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
                    return Ok(new {token });
                }
            }
            return BadRequest(new { message = "invalid email or password" });
        }
        [HttpGet("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return NoContent();
        }

        [Authorize]
        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest changePasswordRequest) {

            var applicationUser = await userManager.GetUserAsync(User);
            if (applicationUser != null) {
               var result = await userManager.ChangePasswordAsync(applicationUser, changePasswordRequest.OldPassword, changePasswordRequest.NewPassword);
                if (result.Succeeded)
                {
                    return NoContent();
                }
                else {
                    return BadRequest(result.Errors);
                
                }

            }

            return BadRequest(new { message = "invalid data" });
            
        }

    }
}
