using AutoMapper;
using MediatR;
using MoneyTracker.Application.DTOs;
using MoneyTracker.Application.Queries.Category;
using MoneyTracker.Application.RepositoryContracts;
using MoneyTracker.Domain;

namespace MoneyTracker.Application.Handler.Category
{
    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, CategoryDto?>
    {
        private readonly ICategoriesRepository _repo;
        private readonly IMapper _mapper;
        public GetCategoryByIdQueryHandler(ICategoriesRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<CategoryDto?> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.id <= 0)
            {
                return null;
            }
            var categories = await _repo.GetAll(includeProp: "ParentCategory,SubCategories,Transactions"); 
            if (categories == null)
            {
                return null;
            }
            var category = await _repo.Get(c => c.Id == request.id, includeProp: "ParentCategory,SubCategories,Transactions"); 
            if (category == null)
            {
                return null;
            }
            CategoryDto categoryDto = _mapper.Map<CategoryDto>(category);
            return categoryDto;
        }
    }
}
