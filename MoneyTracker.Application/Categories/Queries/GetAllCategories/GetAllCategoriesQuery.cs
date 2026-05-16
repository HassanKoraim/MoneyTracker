using MediatR;
using MoneyTracker.Application.DTOs.CategoryDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTracker.Application.Categories.Queries.GetAllCategories
{
    public record GetAllCategoriesQuery() : IRequest<List<CategoryDto>>;
}
