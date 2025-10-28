using Microsoft.EntityFrameworkCore.Update.Internal;
using MoneyTracker_API.Models;

namespace MoneyTracker_API.RepositoryContracts
{
    public interface ITransactionRepository : IRepositoryContracts<Transaction>
    {
        public Task<Transaction> Update(Transaction transaction);
    }
}
