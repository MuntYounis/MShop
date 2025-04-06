using System.ComponentModel.DataAnnotations;

namespace MShop.API.DTOs.Requests
{
    public class CategoryRequest
    {
        [Required(ErrorMessage ="Name is required!!!")]
        [MinLength(2)]
        [MaxLength(5)]
        public string Name { get; set; }
        public string Description { get; set; }

        public bool Status { get; set; }
    }
}
