using Microsoft.EntityFrameworkCore.Update.Internal;
using MoneyTracker_API.Models;
using System.Linq.Expressions;

namespace MoneyTracker_API.RepositoryContracts
{
    public interface ITransactionRepository : IRepositoryContracts<Transaction>
    {
        public Task<Transaction> Update(int id, Transaction transaction);
        public Task<decimal> GetAmount(Expression<Func<Transaction, bool>> filter = null, string transactionType = null);

    }
}
