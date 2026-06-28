using AutoMapper;
using MediatR;
using MoneyTracker.Application.DTOs.TransactionDTOs;
using MoneyTracker.Application.RepositoryContracts;
using MoneyTracker.Domain.Entities;

namespace MoneyTracker.Application.Transactions.Commands.UpdateTransaction
{
    public class UpdateTransactionCommandHandler : IRequestHandler<UpdateTransactionCommand, TransactionDto>
    {
        private readonly ITransactionRepository _repo;
        private readonly ICategoriesRepository _categoryRepo;
        private readonly IMapper _mapper;
        public UpdateTransactionCommandHandler(ITransactionRepository repo, ICategoriesRepository categoryRepo, IMapper mapper)
        {
            _repo = repo;
            _categoryRepo = categoryRepo;
            _mapper = mapper;
        }
        public async Task<TransactionDto> Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
        {
            var transactionInDb = await _repo.Get(t => t.Id == request.id);
            if (transactionInDb == null)
            {
                throw new ArgumentNullException(nameof(transactionInDb));
            }
           
            var transactionFromDto = _mapper.Map<Transaction>(request.transactionUpdateDto);
            if (transactionFromDto.CategoryId != transactionInDb.CategoryId)
            {
                Category? category = await _categoryRepo.Get(c => c.Id == transactionFromDto.CategoryId);
                if (category == null)
                {
                    throw new KeyNotFoundException(nameof(category));
                }
                transactionFromDto.CategoryId = category.Id;
                transactionFromDto.Category = category;
                transactionFromDto.transactionType = category.Type; 
            }
            var transactionUpdated = await _repo.Update(request.id, transactionFromDto);
            TransactionDto transactionDto = _mapper.Map<TransactionDto>(transactionUpdated);
            transactionDto.CategoryName = transactionUpdated.Category.Name;
            return transactionDto;
        }
    }
}
