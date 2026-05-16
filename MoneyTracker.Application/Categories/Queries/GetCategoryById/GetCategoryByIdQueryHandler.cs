using AutoMapper;
using Humanizer;
using MediatR;
using MoneyTracker.Application.DTOs.CategoryDTOs;
using MoneyTracker.Application.RepositoryContracts;
using MoneyTracker.Domain;

namespace MoneyTracker.Application.Categories.Queries.GetCategoryById
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
            var category = await _repo.Get(c => c.Id == request.id, includeProp: "ParentCategory,SubCategories,Transactions"); 
            if (category == null)
            {
                throw new KeyNotFoundException("Category Not Found!");
            }
            CategoryDto categoryDto = _mapper.Map<CategoryDto>(category);
            return categoryDto;
        }
    }
}
