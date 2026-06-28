using FluentValidation;

namespace MoneyTracker.Application.Transactions.Commands.DeleteTransactionById
{
    public class DeleteTransactionCommandValidator : AbstractValidator<DeleteTransactionByIdCommand>
    {
        public DeleteTransactionCommandValidator()
        {
            RuleFor(x => x.id).GreaterThan(0).WithMessage("Id must be greater than zero.")
                .NotNull().WithMessage("Id must be provided.");
        }
    }
}
