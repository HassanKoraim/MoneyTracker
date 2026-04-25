using AutoMapper;
using MediatR;
using MoneyTracker.Application.DTOs;
using MoneyTracker.Application.Queries.Transaction;
using MoneyTracker.Application.RepositoryContracts;

namespace MoneyTracker.Application.Handler.Transaction
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
            if (request.id <= 0)
            {
                throw new ArgumentException("Id can't be less than or equal Zero");
            }
            Domain.Entities.Transaction? transaction = await _repo.Get(t => t.Id == request.id, "Category,PaymentMethod");
            if (transaction == null)
            {
                throw new ArgumentNullException("The Transaction Not Found");
            }
            var transactionDto = _mapper.Map<TransactionDto>(transaction);
            return transactionDto;
        }
    }
}
