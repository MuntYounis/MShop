using MShop.API.Models;
using System.ComponentModel.DataAnnotations;
using MShop.API.Validations;

namespace MShop.API.DTOs.Requests
{
    public class RegisterRequest
    {

        [MinLength(5)]
        public string FirstName { get; set; }

        [MinLength(5)]
        public string LastName { get; set; }

        [MinLength(6)]
        public string UserName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }

        [Compare(nameof(Password),ErrorMessage ="Passwords need to match")]
        public string ConfirmPassword { get; set; }

        public ApplicationUserGender Gender { get; set; }

        [Over18Years]
        public DateTime BirthOfDate { get; set; }

    }
}
