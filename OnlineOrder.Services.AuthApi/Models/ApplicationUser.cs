using Microsoft.AspNetCore.Identity;

namespace OnlineOrder.Services.AuthApi.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string Name { get; set; }
    }
}
