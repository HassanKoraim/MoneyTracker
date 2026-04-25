using MediatR;
using MoneyTracker.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTracker.Application.Commands.Transaction
{
    public record UpdateTransactionCommand(int id, TransactionUpdateDto transactionUpdateDto) : IRequest<TransactionDto>;
}
