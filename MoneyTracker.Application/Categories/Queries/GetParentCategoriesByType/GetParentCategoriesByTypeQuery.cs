using MediatR;
using MoneyTracker.Application.DTOs.CategoryDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MoneyTracker.Domain.Enums.SD;

namespace MoneyTracker.Application.Categories.Queries.GetParentCategoriesByType
{
    public record GetParentCategoriesByTypeQuery(CategoryType? CategoryType) : IRequest<List<CategoryDto>>;
}
