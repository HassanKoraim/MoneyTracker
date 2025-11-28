using Microsoft.EntityFrameworkCore;
using MoneyTracker_API.Data;
using MoneyTracker_API.Models;
using MoneyTracker_API.RepositoryContracts;
using MoneyTracker_Utility;
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
        public async Task<Transaction> Update(int id ,Transaction transaction)
        {
            var transactionFromDb =
               await _context.Transactions.Where(t => t.Id == id).FirstOrDefaultAsync();
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
            transactionFromDb.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync();
            return transactionFromDb;
        }
        public async Task<decimal> GetAmount(Expression<Func<Transaction, bool>> filter = null, string transactionType = null)
        {
            IQueryable<Transaction> query = _context.Transactions;
            if(transactionType != null)
            {
                query = query.Where(t => t.transactionType.ToString() == transactionType);
            }
            if(filter != null)
            {
                query = query.Where(filter);
            }
            decimal totalIncome = await query.SumAsync(t => t.Amount);
            return totalIncome;
        }


    }
}
