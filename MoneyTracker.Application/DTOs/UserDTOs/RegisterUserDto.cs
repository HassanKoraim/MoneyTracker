using static MoneyTracker.Domain.Enums.SD;

namespace MoneyTracker.Application.DTOs.UserDTOs
{
    public record RegisterUserDto
    {
        public Gender Gender { get; set; }
        public RoleType Role { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
    }
}
