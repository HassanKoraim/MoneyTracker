using Microsoft.EntityFrameworkCore;
using MoneyTracker_API.Data;
using MoneyTracker_API.DTOs;
using MoneyTracker_API.Models;
using MoneyTracker_API.RepositoryContracts;

namespace MoneyTracker_API.Repositroies
{
    public class LocalUserRepo : ILocalUserRepo
    {
        private readonly ApplicationDbContext _dbContext;
        public LocalUserRepo(ApplicationDbContext dbcontext)
        {
            _dbContext = dbcontext;
        }
        public async Task<bool> IsUniqueUser(string username)
        {
            var user = await _dbContext.LocalUsers.FirstOrDefaultAsync(u => u.UserName == username);
            return user == null? true : false;
        }

        public async Task<LocalUser?> GetUserByUsernameAndPassword(string UserName, string Password)
        {
            return await _dbContext.LocalUsers.FirstOrDefaultAsync(u => u.UserName.ToLower() == UserName.ToLower() && u.Password == Password);
        }
        public async Task<LocalUser> Register(RegisterationRequestDTO registerationRequest)
        {
            var user =  new LocalUser()
            {
                UserName = registerationRequest.UserName,
                Name = registerationRequest.Name,
                Password = registerationRequest.Password,
                Role = registerationRequest.Role
            };
            _dbContext.LocalUsers.Add(user);
            _dbContext.SaveChanges();
            user.Password = "";
            return user;
        }
    }
}
