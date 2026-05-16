using MediatR;
using MoneyTracker.Application.DTOs.CategoryDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTracker.Application.Categories.Queries.GetParentCategories
{
    /// <summary>
    /// To Return All Parent Categories Type
    /// </summary>
    /// <returns>Returns All Parent Categories</returns>
    public record GetParentCategoriesQuery : IRequest<List<CategoryDto>>;
}
