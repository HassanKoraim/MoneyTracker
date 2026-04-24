using AutoMapper;
using MediatR;
using MoneyTracker.Application.DTOs;
using MoneyTracker.Application.Queries.Category;
using MoneyTracker.Application.RepositoryContracts;

namespace MoneyTracker.Application.Handler.Category
{
    public class GetSubCategoriesByParentIdQueryHandler : IRequestHandler<GetSubCategoriesByParentIdQuery, List<CategoryDto>>
    {
        private readonly ICategoriesRepository _repo;
        private readonly IMapper _mapper;
        public GetSubCategoriesByParentIdQueryHandler(ICategoriesRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<List<CategoryDto>> Handle(GetSubCategoriesByParentIdQuery request, CancellationToken cancellationToken)
        {

            var subCategories =
                await _repo.GetAll(c => c.ParentCategoryId == request.ParentCategoryId);
            if (subCategories == null || !subCategories.Any())
            {
                return new List<CategoryDto>();
            }
            List<CategoryDto> subCategoriesDto =
                _mapper.Map<List<CategoryDto>>(subCategories);
            return subCategoriesDto;
        }
    }
}
