using AutoMapper;
using MoneyTracker_API.DTOs;
using MoneyTracker_API.Models;
using MoneyTracker_API.RepositoryContracts;
using MoneyTracker_API.ServiceContracts;
using System.Linq.Expressions;

namespace MoneyTracker_API.Services
{
    public class TransactionService : ITransactionService
    {
        private ITransactionRepository _repo;
        private IMapper _mapper;
        public TransactionService(ITransactionRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<TransactionDto> GetById(int id)
        {
            if(id <= 0)
            {
                throw new ArgumentException("Id can't be less than or equal Zero");
            }
            Transaction? transaction = await _repo.Get(t => t.Id == id);
            if(transaction == null)
            {
                throw new ArgumentNullException(nameof(transaction));
            }
            var transactionDto = _mapper.Map<TransactionDto>(transaction);
            return transactionDto;
        }

        public async Task<List<TransactionDto>> GetAll(Expression<Func<Transaction, bool>> predicate = null)
        {
            var transactionList = await _repo.GetAll(predicate);
            if (transactionList == null)
            {
                return new List<TransactionDto>();
            }
            var transactionDtoList = _mapper.Map<List<TransactionDto>>(transactionList);
            return transactionDtoList;
        }
        public async Task<TransactionDto> Create(TransactionCreateDto transactionCreateDto)
        {
            if(transactionCreateDto == null)
            {
                throw new ArgumentNullException(nameof(transactionCreateDto));
            }
            Transaction transaction = _mapper.Map<Transaction>(transactionCreateDto);
            var transactionCreated = await _repo.Create(transaction);
            return _mapper.Map<TransactionDto>(transactionCreated);
        }

        public async Task<bool> Delete(int id)
        {
            if(id <= 0)
            {
                throw new ArgumentException("Id can't be less than or equal Zero");
            }
            var transaction = await _repo.Get(t => t.Id == id);
            if (transaction == null)
            {
                throw new ArgumentNullException("We can't found the Transaction");
            }
            return await _repo.Delete(transaction);
        }
        public async Task<TransactionDto> Update(int id, TransactionUpdateDto transactionUpdateDto)
        {
            if(id <= 0)
            {
                throw new ArgumentException("Id can't be less than or equal Zero");
            }
            Transaction? transaction = await _repo.Get(t => t.Id == id);
            if (transaction == null)
            {
                throw new ArgumentNullException(nameof(transaction));
            }
            transaction.Amount = transactionUpdateDto.Amount;
            transaction.Description = transactionUpdateDto.Description;
            transaction.transactionType = transactionUpdateDto.TransactionType;
            transaction.TransactionDate = transactionUpdateDto.TransactionDate;
            transaction.CategoryId = transactionUpdateDto.CategoryId;
            transaction.PaymentMethodId = transactionUpdateDto.PaymentMethodId;
            transaction.IsRecurring = transactionUpdateDto.IsRecurring;
            transaction.RecurrenceType = transactionUpdateDto.RecurrenceType;
            transaction.RecurrenceEndDate = transactionUpdateDto.RecurrenceEndDate;
            transaction.ImageUrl = transactionUpdateDto.ImageUrl;
            transaction.UpdatedAt = DateTime.Now;
            TransactionDto transactionDto = _mapper.Map<TransactionDto>(transaction);
            return transactionDto;
        }

    }
}
