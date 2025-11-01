using MoneyTracker_API.DTOs;
using System.Linq.Expressions;
using MoneyTracker_API.Models;

namespace MoneyTracker_API.ServiceContracts
{
    public interface ITransactionService
    {
        // GetTransactionById
        public Task<TransactionDto> GetById(int id);
        public Task<List<TransactionDto>> GetAll(Expression<Func<Transaction,bool>> predicate = null);
        //Create
        public Task<TransactionDto> Create(TransactionCreateDto transactionCreateDto);
        //Update
        public Task<TransactionDto> Update(int id, TransactionUpdateDto transactionUpdateDto);
        //Delete
        public Task<bool> Delete(int id);

        //GetIncomeInSpecificCategory

        //GetAllIncome

        //GetExpenseInSpecificCategory

        //GetAllExpense


    }
}
