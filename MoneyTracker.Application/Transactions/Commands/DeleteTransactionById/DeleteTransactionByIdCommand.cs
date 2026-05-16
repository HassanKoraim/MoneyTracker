using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTracker.Application.Transactions.Commands.DeleteTransactionById
{
    public record DeleteTransactionByIdCommand(int id) : IRequest<bool>;
}
