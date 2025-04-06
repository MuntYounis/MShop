using System.ComponentModel.DataAnnotations;
namespace MShop.API.DTOs.Requests
{
    public class BrandRequest
    {
        [Required(ErrorMessage = "Name is required!!!")]
        [MinLength(2)]
        public string Name { get; set; }
        public string Description { get; set; }


    }
}
