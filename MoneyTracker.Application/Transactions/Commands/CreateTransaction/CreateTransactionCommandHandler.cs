using AutoMapper;
using MediatR;
using MoneyTracker.Application.DTOs.TransactionDTOs;
using MoneyTracker.Application.RepositoryContracts;
using MoneyTracker.Domain.Entities;

namespace MoneyTracker.Application.Transactions.Commands.CreateTransaction
{
    public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, TransactionDto>
    {
        private readonly ITransactionRepository _repo;
        private readonly ICategoriesRepository _categoryRepo;
        private readonly IMapper _mapper;
        public CreateTransactionCommandHandler(ITransactionRepository repo, ICategoriesRepository categoryRepo, IMapper mapper)
        {
            _repo = repo;
            _categoryRepo = categoryRepo;
            _mapper = mapper;
        }
        public async Task<TransactionDto> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            Category? category = await _categoryRepo.Get(c => c.Id == request.transactionCreateDto.CategoryId);
            if(category == null)
            {
                throw new KeyNotFoundException(nameof(category));
            }
            var transaction = _mapper.Map<Transaction>(request.transactionCreateDto);
            transaction.CategoryId = category.Id;
            transaction.transactionType = category.Type;
            transaction.CreatedAt = DateTime.UtcNow;
            var transactionCreated = await _repo.Create(transaction);
            TransactionDto transactionDto = _mapper.Map<TransactionDto>(transactionCreated);
            transactionDto.CategoryName = category.Name;
            return transactionDto;
        }
    }
}
