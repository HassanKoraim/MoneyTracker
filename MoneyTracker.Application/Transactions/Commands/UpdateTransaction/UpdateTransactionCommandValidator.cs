using FluentValidation;

namespace MoneyTracker.Application.Transactions.Commands.UpdateTransaction
{
    public class DeleteTransactionCommandValidator : AbstractValidator<UpdateTransactionCommand>
    {
        public DeleteTransactionCommandValidator()
        {
            RuleFor(x => x.transactionUpdateDto).NotNull().WithMessage("Transaction Modle must be provided.");
            RuleFor(x => x.id).GreaterThan(0).WithMessage("Id must be greater than zero.")
                .NotNull().WithMessage("Id must be provided.");
            RuleFor(x => x.transactionUpdateDto.Amount).NotNull().WithMessage("Transaction Amound must be provided.")
                .GreaterThan(0).WithMessage("Amount must be greater than zero.");
            RuleFor(x => x.transactionUpdateDto.TransactionDate).NotNull().WithMessage("Transaction Date must be provided.");
            RuleFor(x => x.transactionUpdateDto.CategoryId).NotNull().WithMessage("Category must be provided.")
                .GreaterThan(0).WithMessage("CategoryId must be greater than zero.");
            RuleFor(x => x.transactionUpdateDto.PaymentMethodId).NotNull().WithMessage("Payment Method must be provided.")
                .GreaterThan(0).WithMessage("PaymentMethodId must be greater than zero.");
        }
    }
}
