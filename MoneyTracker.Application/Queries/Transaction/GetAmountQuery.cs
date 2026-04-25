using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTracker.Application.Queries.Transaction
{
    public record GetAmountQuery(Expression<Func<Domain.Entities.Transaction, bool>> filter = null, string transactionType = null) : IRequest<decimal>;
}
