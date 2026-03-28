using MoneyTracker_API.Models;

namespace MoneyTracker_API.DTOs
{
    public class LoginResponseDTO
    {
        public LocalUser User { get; set; }
        public string Token { get; set; }
    }
}
