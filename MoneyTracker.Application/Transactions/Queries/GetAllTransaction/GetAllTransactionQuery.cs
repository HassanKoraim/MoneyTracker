using MediatR;
using MoneyTracker.Application.DTOs.TransactionDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTracker.Application.Transactions.Queries.GetAllTransaction
{
    public record GetAllTransactionQuery(
        Expression<Func<Domain.Entities.Transaction, bool>> predicate = null,
        string? sortBy = null, string? sortOrder = null) 
        : IRequest<List<TransactionDto>>;
}
