using MoneyTracker.Application.RepositoryContracts;
using MoneyTracker.Domain.Entities.Identity;
using MoneyTracker.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTracker.Infrastructure.Repositroies
{
    public class AuthRepository : Repository<User>, IAuthRepository
    {
        public AuthRepository(ApplicationDbContext context) : base(context)
        {
            
        }
    }
}
