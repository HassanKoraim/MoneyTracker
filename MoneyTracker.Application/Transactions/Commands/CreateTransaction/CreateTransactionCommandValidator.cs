using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTracker.Application.Transactions.Commands.CreateTransaction
{
    public class UpdateTransactionCommandValidator : AbstractValidator<CreateTransactionCommand>
    {
        public UpdateTransactionCommandValidator()
        {
            RuleFor(x => x.transactionCreateDto).NotNull().WithMessage("Transaction Modle must be provided.");
            RuleFor(x => x.transactionCreateDto.Amount).NotNull().WithMessage("Transaction Amound must be provided.")
                .GreaterThan(0).WithMessage("Amount must be greater than zero.");
            RuleFor(x => x.transactionCreateDto.TransactionDate).NotNull().WithMessage("Transaction Date must be provided.");
            RuleFor(x => x.transactionCreateDto.CategoryId).NotNull().WithMessage("Category must be provided.")
                .GreaterThan(0).WithMessage("CategoryId must be greater than zero.");
            RuleFor(x => x.transactionCreateDto.PaymentMethodId).NotNull().WithMessage("Payment Method must be provided.")
                .GreaterThan(0).WithMessage("PaymentMethodId must be greater than zero.");
        }
    }
}
