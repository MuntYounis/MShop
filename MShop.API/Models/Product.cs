using System.ComponentModel.DataAnnotations.Schema;

namespace MShop.API.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        [Column(TypeName = "decimal(5,2)")]
        public decimal Discount { get; set; }

        public string? mainImg { get; set; }

        public int Quantity {  get; set; }

        public double Rate { get; set; }

        public bool Status { get; set; }

        public Category Category { get; set; }

        public int CategoryId { get; set; }

        public Brand Brand { get; set; }

        public int? BrandId { get; set; }
    }
}
