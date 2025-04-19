using MShop.API.Models;
using MShop.API.Validations;
using System.ComponentModel.DataAnnotations;

namespace MShop.API.DTOs.Requests
{
    public class LoginRequest
    {


        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }

        public bool RememberMe { get; set; }



    }
}
