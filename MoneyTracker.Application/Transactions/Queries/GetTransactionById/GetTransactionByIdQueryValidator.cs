using FluentValidation;
using MediatR;
using MoneyTracker.Application.DTOs.TransactionDTOs;

namespace MoneyTracker.Application.Transactions.Queries.GetTransactionById
{
    public class GetTransactionByIdQueryValidator : AbstractValidator<GetTransactionByIdQuery>
    {
      public GetTransactionByIdQueryValidator()
        {
            RuleFor(x => x.id).GreaterThan(0).WithMessage("Id must be greater than zero.")
                .NotNull().WithMessage("Id must be provided.");
        }
    }
}
