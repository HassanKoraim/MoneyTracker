using AutoMapper;
using MediatR;
using MoneyTracker.Application.DTOs;
using MoneyTracker.Application.Queries.Category;
using MoneyTracker.Application.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTracker.Application.Handler.Category
{
    public class GetParentCategoriesByTypeQueryHandler : IRequestHandler<GetParentCategoriesByTypeQuery, List<CategoryDto>>
    {
        private readonly ICategoriesRepository _repo;
        private readonly IMapper _mapper;
        public GetParentCategoriesByTypeQueryHandler(ICategoriesRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<List<CategoryDto>> Handle(GetParentCategoriesByTypeQuery request, CancellationToken cancellationToken)
        {
            if (request.CategoryType == null)
            {
                return null; // await GetParentCategories();
            }
            var parentCategories =
                await _repo.GetParentCategoriesByType(request.CategoryType);
            if (parentCategories == null || !parentCategories.Any())
            {
                return new List<CategoryDto>();  // Empty List
            }
            var parentCategoriesDto =
                 _mapper.Map<List<CategoryDto>>(parentCategories);
            return parentCategoriesDto;
        }
    }
}
