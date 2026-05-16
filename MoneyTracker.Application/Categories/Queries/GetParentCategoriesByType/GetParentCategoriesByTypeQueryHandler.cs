using AutoMapper;
using MediatR;
using MoneyTracker.Application.DTOs.CategoryDTOs;
using MoneyTracker.Application.RepositoryContracts;

namespace MoneyTracker.Application.Categories.Queries.GetParentCategoriesByType
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
            //if (request.CategoryType == null)
            //{
            //    return null; // await GetParentCategories();
            //}
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
