using MediatR;
using MoneyTracker.Application.DTOs.CategoryDTOs;

namespace MoneyTracker.Application.Categories.Queries.GetSubCategoriesByParentId
{
    public record GetSubCategoriesByParentIdQuery(int? ParentCategoryId) : IRequest<List<CategoryDto>>;
}
