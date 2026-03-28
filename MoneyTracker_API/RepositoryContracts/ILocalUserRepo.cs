using MoneyTracker_API.DTOs;
using MoneyTracker_API.Models;

namespace MoneyTracker_API.RepositoryContracts
{
    public interface ILocalUserRepo
    {
        public Task<bool> IsUniqueUser(string username);
        //public Task<LoginResponseDTO> Login(LoginRequestDTO loginRequest);
        public Task<LocalUser?> GetUserByUsernameAndPassword(string username, string password);
        public Task<LocalUser> Register(RegisterationRequestDTO registerationRequest);
    }
}
