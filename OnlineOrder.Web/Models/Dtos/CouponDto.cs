using System.ComponentModel.DataAnnotations;

namespace OnlineOrder.Web.Models.Dtos
{
    public class CouponDto
    {
        public int Id { get; set; }
        public string? Code { get; set; }
        public double DiscountAmount { get; set; }
        [Required]
        public double MinAmount { get; set; }
    }
}
