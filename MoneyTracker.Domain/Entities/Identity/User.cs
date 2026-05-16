using Microsoft.AspNetCore.Identity;

namespace MoneyTracker.Domain.Entities.Identity
{
    public class User : IdentityUser
    {
        public string Address { get; set; }
        public string Country { get; set; }
    }
}
