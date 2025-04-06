using MShop.API.Models;
using System.ComponentModel.DataAnnotations;

namespace MShop.API.DTOs.Requests
{
    public class ProductRequest
    {
        [Required(ErrorMessage = "Name is required!!!")]
        public string Name { get; set; }
        public string Description { get; set; }

        public decimal Price { get; set; }
        public decimal Discount { get; set; }

        public IFormFile mainImg { get; set; }

        public int Quantity { get; set; }

        public double Rate { get; set; }

        public bool Status { get; set; }



        public int CategoryId { get; set; }
        public int BrandId { get; set; }

    }
}
