using System.ComponentModel.DataAnnotations;

namespace OnlineOrder.Services.ProductApi.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Price { get; set; }
        public string Description { get; set; }
        [Required]
        public string CategoryName { get; set; }
        public string ImageUrl { get; set; }
    }
}
