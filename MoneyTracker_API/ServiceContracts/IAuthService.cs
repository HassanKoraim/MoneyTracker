using MoneyTracker_API.DTOs;
using MoneyTracker_API.Models;

namespace MoneyTracker_API.ServiceContracts
{
    public interface IAuthService
    {
        public Task<LoginResponseDTO> Login (LoginRequestDTO loginRequestDTO);
        public Task<LocalUser>? Register(RegisterationRequestDTO registerationRequestDTO);
    }
}
