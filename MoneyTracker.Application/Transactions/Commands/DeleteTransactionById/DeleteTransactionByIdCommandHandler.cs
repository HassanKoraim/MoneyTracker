using AutoMapper;
using MediatR;
using MoneyTracker.Application.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTracker.Application.Transactions.Commands.DeleteTransactionById
{
    public class DeleteTransactionByIdCommandHandler : IRequestHandler<DeleteTransactionByIdCommand, bool>
    {
        private readonly ITransactionRepository _repo;
        public DeleteTransactionByIdCommandHandler(ITransactionRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(DeleteTransactionByIdCommand request, CancellationToken cancellationToken)
        {
            if (request.id <= 0)
            {
                throw new ArgumentException("Id can't be less than or equal Zero");
            }
            var transaction = await _repo.Get(t => t.Id == request.id);
            if (transaction == null)
            {
                throw new ArgumentNullException("We can't found the Transaction");
            }
            return await _repo.Delete(transaction);
        }
    }
}
