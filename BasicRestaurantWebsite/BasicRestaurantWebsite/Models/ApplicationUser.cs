using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BasicRestaurantWebsite.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Order>? Orders {  get; set; }

    }
}
