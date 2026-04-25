using AutoMapper;
using MediatR;
using MoneyTracker.Application.Commands.Transaction;
using MoneyTracker.Application.DTOs;
using MoneyTracker.Application.RepositoryContracts;

namespace MoneyTracker.Application.Handler.Transaction
{
    public class UpdateTransactionCommandHandler : IRequestHandler<UpdateTransactionCommand, TransactionDto>
    {
        private readonly ITransactionRepository _repo;
        private readonly IMapper _mapper;
        public UpdateTransactionCommandHandler(ITransactionRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<TransactionDto> Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
        {
            if (request.id <= 0)
            {
                throw new ArgumentException("Id can't be less than or equal Zero");
            }
            var transaction = await _repo.Get(t => t.Id == request.id);
            if (transaction == null)
            {
                throw new ArgumentNullException(nameof(transaction));
            }
            var transactionFromDto = _mapper.Map<Domain.Entities.Transaction>(request.transactionUpdateDto);
            var transactionUpdated = await _repo.Update(request.id, transactionFromDto);
            TransactionDto transactionDto = _mapper.Map<TransactionDto>(transactionUpdated);
            return transactionDto;
        }
    }
}
