using System.ComponentModel.DataAnnotations;

namespace OnlineOrder.Services.CouponApi.Models.Dto
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
