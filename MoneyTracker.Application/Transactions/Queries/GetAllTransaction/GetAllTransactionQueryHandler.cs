using AutoMapper;
using MediatR;
using MoneyTracker.Application.DTOs.TransactionDTOs;
using MoneyTracker.Application.RepositoryContracts;
using System.Globalization;

namespace MoneyTracker.Application.Transactions.Queries.GetAllTransaction
{
    public class GetAllTransactionQueryHandler : IRequestHandler<GetAllTransactionQuery, List<TransactionDto>>
    {
        private readonly ITransactionRepository _repo;
        private readonly IMapper _mapper;
        public GetAllTransactionQueryHandler(ITransactionRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<List<TransactionDto>> Handle(GetAllTransactionQuery request, CancellationToken cancellationToken)
        {
            var transactionList = await _repo.GetTransactions(request.predicate, "Category,PaymentMethod");
            if (transactionList == null)
            {
                return new List<TransactionDto>();
            }
            var transactionDtoList = _mapper.Map<List<TransactionDto>>(transactionList);
            if (request.sortBy != null && request.sortOrder != null)
            {
                transactionDtoList = Sort(request.sortBy, request.sortOrder, transactionDtoList);
            }
            return transactionDtoList;
        }
        public List<TransactionDto> Sort(string sortBy, string sortOrder, List<TransactionDto> transactions)
        {
            // 1. Validation: If list is null or empty, return as is
            if (transactions == null || !transactions.Any()) return transactions;

            // 2. Normalize inputs to avoid case-sensitivity issues
            // string sort = sortBy?.ToLower();
            string order = sortOrder?.ToLower();

            // 3. Return the sorted list
            return (sortBy, order) switch
            {
                (nameof(Domain.Entities.Transaction.Amount), "asc") => transactions.OrderBy(t => t.Amount).ToList(),
                (nameof(Domain.Entities.Transaction.Amount), "desc") => transactions.OrderByDescending(t => t.Amount).ToList(),
                (nameof(Domain.Entities.Transaction.TransactionDate), "asc") => transactions.OrderBy(t => t.TransactionDate).ToList(),
                (nameof(Domain.Entities.Transaction.TransactionDate), "desc") => transactions.OrderByDescending(t => t.TransactionDate).ToList(),
                _ => transactions // Default: No sorting
            };
        }
    }
}
