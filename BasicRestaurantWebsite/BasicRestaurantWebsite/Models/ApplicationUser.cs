using Microsoft.AspNetCore.Identity;

namespace BasicRestaurantWebsite.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Order>? Orders {  get; set; }
    }
}
