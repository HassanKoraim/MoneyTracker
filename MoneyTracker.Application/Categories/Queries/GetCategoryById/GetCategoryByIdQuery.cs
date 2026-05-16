using MediatR;
using MoneyTracker.Application.DTOs.CategoryDTOs;

namespace MoneyTracker.Application.Categories.Queries.GetCategoryById
{
    public record GetCategoryByIdQuery(int id) : IRequest<CategoryDto?>;
}
