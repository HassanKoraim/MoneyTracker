using MediatR;
using MoneyTracker.Application.Queries.Transaction;
using MoneyTracker.Application.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MoneyTracker.Domain.Enums.SD;
using static System.Net.WebRequestMethods;

namespace MoneyTracker.Application.Handler.Transaction
{
    public class GetAmountQueryHandler : IRequestHandler<GetAmountQuery, decimal>
    {
        private readonly ITransactionRepository _repo;
        public GetAmountQueryHandler(ITransactionRepository repo)
        {
            _repo = repo;
        }

        public async Task<decimal> Handle(GetAmountQuery request, CancellationToken cancellationToken)
        {
            return await _repo.GetAmount(request.filter, request.transactionType);
        }
    }
}
