using Humanizer;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MoneyTracker.Application.DTOs.UserDTOs;
using MoneyTracker.Application.RepositoryContracts;
using MoneyTracker.Domain.Entities.Identity;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace MoneyTracker.Application.Auth.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, string>
    {
        private readonly IAuthRepository _userRepo;
        private readonly IConfiguration _config;
        public LoginCommandHandler(IAuthRepository userRepo, IConfiguration config)
        {
            _userRepo = userRepo;
            _config = config;
        }
        public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            User? userFromDb = await _userRepo.Get(u => u.Email == request.loginDto.Email);
            if(userFromDb == null)
            {
                throw new ArgumentException("Email Can't be found");
            }
            if(new PasswordHasher<User>().VerifyHashedPassword(userFromDb, userFromDb.PasswordHash, request.loginDto.Password)
                == PasswordVerificationResult.Failed)
            {
                throw new ArgumentException("Password is in correct!");
            }
            string token = CreateToken(userFromDb);
            return token;
        }
        private string CreateToken(User user)
        {
            var Claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["AppSettings:Token"]!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
            var tokenDescriptor = new JwtSecurityToken
                (
                    issuer: _config["AppSettings:Issuer"],
                    audience: _config["AppSettings:Audience"],
                    claims: Claims,
                    expires: DateTime.UtcNow.AddDays(1),
                    signingCredentials: creds
                );
            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
    }
}
