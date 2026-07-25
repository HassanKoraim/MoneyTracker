using MoneyTracker.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTracker.Application.RepositoryContracts
{
    public interface IAuthRepository : IRepositoryContracts<User>
    {
    }
}
