using AutoMapper;
using MediatR;
using MoneyTracker.Application.DTOs.TransactionDTOs;
using MoneyTracker.Application.RepositoryContracts;
using MoneyTracker.Domain.Entities;

namespace MoneyTracker.Application.Transactions.Queries.GetTransactionById
{
    public class GetTransactionByIdQueryHandler : IRequestHandler<GetTransactionByIdQuery, TransactionDto>
    {
        private readonly ITransactionRepository _repo;
        private readonly IMapper _mapper;
        public GetTransactionByIdQueryHandler(ITransactionRepository repo,IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<TransactionDto> Handle(GetTransactionByIdQuery request, CancellationToken cancellationToken)
        {
            Transaction? transaction = await _repo.Get(t => t.Id == request.id, "Category,PaymentMethod");
            if (transaction == null)
            {
                throw new KeyNotFoundException("The Transaction Not Found");
            }
            var transactionDto = _mapper.Map<TransactionDto>(transaction);
            return transactionDto;
        }
    }
}
