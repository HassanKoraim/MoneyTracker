using MediatR;
using MoneyTracker.Application.DTOs;

namespace MoneyTracker.Application.Queries.Category
{
    public record GetSubCategoriesByParentIdQuery(int ParentCategoryId) : IRequest<List<CategoryDto>>;
}
