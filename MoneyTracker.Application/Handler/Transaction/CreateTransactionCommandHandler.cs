using AutoMapper;
using MediatR;
using MoneyTracker.Application.Commands.Transaction;
using MoneyTracker.Application.DTOs;
using MoneyTracker.Application.RepositoryContracts;

namespace MoneyTracker.Application.Handler.Transaction
{
    public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, TransactionDto>
    {
        private readonly ITransactionRepository _repo;
        private readonly IMapper _mapper;
        public CreateTransactionCommandHandler(ITransactionRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<TransactionDto> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            if (request.transactionCreateDto == null)
            {
                throw new ArgumentNullException(nameof(request.transactionCreateDto));
            }
            if (request.transactionCreateDto.Amount <= 0)
            {
                throw new ArgumentException("Amount must be greater than zero");
            }
            if (request.transactionCreateDto.CategoryId <= 0)
            {
                throw new ArgumentException("CategoryId must be greater than zero");
            }
            if (request.transactionCreateDto.PaymentMethodId <= 0)
            {
                throw new ArgumentException("PaymentMethodId must be greater than zero");
            }
            var transaction = _mapper.Map<Domain.Entities.Transaction>(request.transactionCreateDto);
            var transactionCreated = await _repo.Create(transaction);
            return _mapper.Map<TransactionDto>(transactionCreated);
        }
    }
}
