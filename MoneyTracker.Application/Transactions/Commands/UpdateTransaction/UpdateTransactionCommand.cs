using MediatR;
using MoneyTracker.Application.DTOs.TransactionDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTracker.Application.Transactions.Commands.UpdateTransaction
{
    public record UpdateTransactionCommand(int id, TransactionUpdateDto transactionUpdateDto) : IRequest<TransactionDto>;
}
