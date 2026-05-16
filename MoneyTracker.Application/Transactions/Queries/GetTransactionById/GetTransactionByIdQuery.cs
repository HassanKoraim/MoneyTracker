using MediatR;
using MoneyTracker.Application.DTOs.TransactionDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTracker.Application.Transactions.Queries.GetTransactionById
{
    public record GetTransactionByIdQuery(int id) : IRequest<TransactionDto>;
}
