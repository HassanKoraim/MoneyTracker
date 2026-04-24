using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTracker.Application.Commands.Category
{
    public record DeleteCategoryCommand(int id) : IRequest<bool>;
}
