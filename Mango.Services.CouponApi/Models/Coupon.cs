using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineOrder.Services.CouponApi.Models
{
    public class Coupon
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [Required]
        public string? Code { get; set; }
        [Required]
        public double DiscountAmount{ get; set; }
        [Required]
        public double MinAmount {  get; set; }
    }
}
