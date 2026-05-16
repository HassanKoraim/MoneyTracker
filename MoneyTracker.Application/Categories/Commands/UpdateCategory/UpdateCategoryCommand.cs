using MediatR;
using MoneyTracker.Application.DTOs.CategoryDTOs;

namespace MoneyTracker.Application.Categories.Commands.UpdateCategory
{
    public record UpdateCategoryCommand(int id, CategoryUpdateDto categoryUpdateDto) : IRequest<CategoryDto>;
}
