using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTracker.Application.Categories.Queries.GetSubCategoriesByParentId
{
    public class GetSubCategoriesByParentIdQueryValidator : AbstractValidator<GetSubCategoriesByParentIdQuery>
    {
        public GetSubCategoriesByParentIdQueryValidator()
        {
            RuleFor(x=> x.ParentCategoryId).GreaterThan(0).WithMessage("Parent Category Id should be Greater than 0.")
                .NotNull().WithMessage("Parent Category Id is required.");
        }
    }
}
