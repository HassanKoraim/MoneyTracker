using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTracker.Application.Categories.Queries.GetCategoryById
{
    public class GetCategoryByIdQueryValidator : AbstractValidator<GetCategoryByIdQuery>
    {
        public GetCategoryByIdQueryValidator()
        {
            RuleFor(x => x.id).GreaterThan(0).WithMessage("Id must be greater than 0.");
        }
    }
}
