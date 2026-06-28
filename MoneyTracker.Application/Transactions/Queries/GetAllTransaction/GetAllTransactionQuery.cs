using MediatR;
using MoneyTracker.Application.DTOs.TransactionDTOs;
using MoneyTracker.Domain.Entities;
using System.Linq.Expressions;

namespace MoneyTracker.Application.Transactions.Queries.GetAllTransaction
{
    public record GetAllTransactionQuery(
        Expression<Func<Transaction, bool>> predicate = null,
        string? sortBy = null, string? sortOrder = null) 
        : IRequest<List<TransactionDto>>;
}
