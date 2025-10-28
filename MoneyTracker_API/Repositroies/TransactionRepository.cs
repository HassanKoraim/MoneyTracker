using Microsoft.EntityFrameworkCore;
using MoneyTracker_API.Data;
using MoneyTracker_API.Models;
using MoneyTracker_API.RepositoryContracts;
using System.Linq.Expressions;

namespace MoneyTracker_API.Repositroies
{
    public class TransactionRepository : Repository<Transaction>,ITransactionRepository
    {
        private readonly ApplicationDbContext _context;
        public TransactionRepository(ApplicationDbContext context):base(context)
        {
            _context = context;
        }
        public async Task<Transaction> Update(Transaction transaction)
        {
            var transactionFromDb =
               await _context.Transactions.Where(t => t.Id == transaction.Id).FirstOrDefaultAsync();
            transactionFromDb.Amount = transaction.Amount;
            transactionFromDb.transactionType = transaction.transactionType;
            transactionFromDb.Description = transaction.Description;
            transactionFromDb.TransactionDate = transaction.TransactionDate;
            transactionFromDb.CategoryId = transaction.CategoryId;
            transactionFromDb.PaymentMethodId = transaction.PaymentMethodId;
            transactionFromDb.IsRecurring = transaction.IsRecurring;
            transactionFromDb.RecurrenceType = transaction.RecurrenceType;
            transactionFromDb.RecurrenceEndDate = transaction.RecurrenceEndDate;
            transactionFromDb.ImageUrl = transaction.ImageUrl;
            transactionFromDb.CreatedAt = transaction.CreatedAt;
            transactionFromDb.UpdatedAt = transaction.UpdatedAt;
            await _context.SaveChangesAsync();
            return transactionFromDb;
        }
    }
}
