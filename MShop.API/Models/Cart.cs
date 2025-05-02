namespace MShop.API.Models
{
    public class Cart
    {
        public int ProductId { get; set; }
        public Product product { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public int Count { get; set; }  


    }
}
