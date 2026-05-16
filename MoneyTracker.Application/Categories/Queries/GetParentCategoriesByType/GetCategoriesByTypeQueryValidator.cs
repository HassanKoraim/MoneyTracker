using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTracker.Application.Categories.Queries.GetParentCategoriesByType
{
    public class GetCategoriesByTypeQueryValidator : AbstractValidator<GetParentCategoriesByTypeQuery>
    {
        public GetCategoriesByTypeQueryValidator()
        {
                RuleFor(x => x.CategoryType)
                    .IsInEnum()
                    .WithMessage("Invalid category type. Allowed values are: Income, Expense, Transfer.")
                    .NotNull() .WithMessage("Category type is required.");
        }
    }
}
