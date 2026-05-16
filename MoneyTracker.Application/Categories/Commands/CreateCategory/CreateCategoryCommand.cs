using MediatR;
using MoneyTracker.Application.DTOs.CategoryDTOs;

namespace MoneyTracker.Application.Categories.Commands.CreateCategory
{
    public record CreateCategoryCommand(CategoryCreateDto CategoryCreateDto) : IRequest<CategoryDto>;
}
