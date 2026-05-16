using FluentValidation;


namespace MoneyTracker.Application.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(x => x.CategoryCreateDto).NotNull().WithMessage("Category data must be provided.");
            RuleFor(x => x.CategoryCreateDto.Name)
                .NotEmpty().WithMessage("Category name is required.")
                .Length(2, 50).WithMessage("Category name must be between 2 and 50 characters.");
            RuleFor(x => x.CategoryCreateDto.Type)
                     .Must(type => type == Domain.Enums.SD.CategoryType.Expense || type == Domain.Enums.SD.CategoryType.Income)
                     .WithMessage("Sub-category type must be either Expense or Income.")
                     .NotNull().WithMessage("type can't be Null")
                     .When(x => x.CategoryCreateDto.ParentCategoryId == null);
        }
    }
}
