using MediatR;
using MoneyTracker.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTracker.Application.Queries.Transaction
{
    public record GetTransactionByIdQuery(int id) : IRequest<TransactionDto>;
}
