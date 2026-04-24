using MediatR;
using MoneyTracker.Application.DTOs;

namespace MoneyTracker.Application.Commands.Category
{
    public record UpdateCategoryCommand(int id, CategoryUpdateDto categoryUpdateDto) : IRequest<CategoryDto>;
}
