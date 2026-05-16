using FluentValidation;

namespace MoneyTracker.Application.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidator()
        {
            RuleFor(x => x.categoryUpdateDto.Name).NotEmpty().WithMessage("Category name is required.")
                  .Length(2, 50).WithMessage("Category name must be between 2 and 50 characters.");
            RuleFor(x => x.categoryUpdateDto.Type).NotNull().WithMessage("Category type is required.")
                   .IsInEnum().WithMessage("Category type is invalid.");
            RuleFor(x => x.categoryUpdateDto.ParentCategoryId).GreaterThan(0)
                .When(x => x.categoryUpdateDto.ParentCategoryId.HasValue);
            RuleFor(x => x.categoryUpdateDto.Type)
                    .Must(type => type == Domain.Enums.SD.CategoryType.Expense || type == Domain.Enums.SD.CategoryType.Income)
                    .WithMessage("Sub-category type must be either Expense or Income.")
                    .NotNull().WithMessage("type can't be Null")
                    .When(x => x.categoryUpdateDto.ParentCategoryId == null);
        }
    }
}
