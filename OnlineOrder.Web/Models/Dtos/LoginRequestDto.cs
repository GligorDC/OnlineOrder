using System.ComponentModel.DataAnnotations;

namespace OnlineOrder.Web.Models.Dtos
{
    public class LoginRequestDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
