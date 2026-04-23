using MoneyTracker.Domain.Entities;
using System.Linq.Expressions;

namespace MoneyTracker.Application.RepositoryContracts
{
    public interface ITransactionRepository : IRepositoryContracts<Transaction>
    {
        public Task<List<Transaction>> GetTransactions(Expression<Func<Transaction, bool>> filter = null, string? includePro = null);
        public Task<Transaction> Update(int id, Transaction transaction);
        public Task<decimal> GetAmount(Expression<Func<Transaction, bool>> filter = null, string transactionType = null);

    }
}
