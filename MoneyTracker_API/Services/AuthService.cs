using Microsoft.IdentityModel.Tokens;
using MoneyTracker_API.DTOs;
using MoneyTracker_API.Models;
using MoneyTracker_API.RepositoryContracts;
using MoneyTracker_API.Repositroies;
using MoneyTracker_API.ServiceContracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MoneyTracker_API.Services
{
    public class AuthService : IAuthService
    {
        private readonly ILocalUserRepo _localUserRepo;
        private string secretKey;
        public AuthService(ILocalUserRepo localUserRepo, IConfiguration configuration)
        {
            _localUserRepo = localUserRepo;
            secretKey = configuration.GetValue<string>("ApiSettings:Secret");    
        }
        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            if (loginRequestDTO == null)
            {

                return new LoginResponseDTO()
                {
                    User = null,
                    Token = ""
                };
            }
            var user = await _localUserRepo.GetUserByUsernameAndPassword(loginRequestDTO.UserName, loginRequestDTO.Password);
            if (user == null)
            {
                return new LoginResponseDTO()
                {
                    User = null,
                    Token = ""
                };
            }
            // if user is found then create JWT Token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name,user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            LoginResponseDTO loginResponseDTO = new()
            {
                User = user,
                Token = tokenHandler.WriteToken(token)
            };
            return loginResponseDTO;
        }

        public Task<LocalUser>? Register(RegisterationRequestDTO requestDTO)
        {
            if(requestDTO == null || string.IsNullOrEmpty(requestDTO.UserName) || string.IsNullOrEmpty(requestDTO.Password) || string.IsNullOrEmpty(requestDTO.Role))
            {
                return null;
            }
            return _localUserRepo.Register(requestDTO);
        }
    }
}
