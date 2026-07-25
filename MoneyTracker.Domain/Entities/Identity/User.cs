using Microsoft.AspNetCore.Identity;
using static MoneyTracker.Domain.Enums.SD;

namespace MoneyTracker.Domain.Entities.Identity
{
    public class User : IdentityUser
    {
        public Gender Gender { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public DateTime BirthDate { get; set; }
        public RoleType Role { get; set; }
    }
}
