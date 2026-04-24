using AutoMapper;
using MediatR;
using MoneyTracker.Application.Commands.Category;
using MoneyTracker.Application.DTOs;
using MoneyTracker.Application.RepositoryContracts;

namespace MoneyTracker.Application.Handler.Category
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, CategoryDto>
    {
        private readonly ICategoriesRepository _repo;
        private readonly IMapper _mapper;
        public UpdateCategoryCommandHandler(ICategoriesRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<CategoryDto> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            if (request.categoryUpdateDto == null)
            {
                throw new ArgumentNullException(nameof(request.categoryUpdateDto));
            }
            if (string.IsNullOrWhiteSpace(request.categoryUpdateDto.Name))
            {
                throw new ArgumentException("Category name cannot be empty", nameof(request.categoryUpdateDto.Name));
            }

            if (request.categoryUpdateDto.ParentCategoryId <= 0)
            {
                throw new ArgumentException("Parent category Id Cannot be Equal or less than Zero");
            }
            // Validate parent category exists if provided
            if (request.categoryUpdateDto.ParentCategoryId.HasValue)
            {
                var parentCategory =
                    await _repo.Get(
                        c => c.Id == request.categoryUpdateDto.ParentCategoryId
                        && c.ParentCategoryId == null);  //check for exists the Category in db and didn't subcategory
                if (parentCategory == null)
                {
                    throw new ArgumentException("Parent category not found", nameof(request.categoryUpdateDto.ParentCategoryId));
                }
            }
            Domain.Entities.Category category = await _repo.Get(c => c.Id == request.id);
            _mapper.Map(request.categoryUpdateDto, category);
            Domain.Entities.Category? categoryUpdated = await _repo.Update(category);
            CategoryDto categoryDto = _mapper.Map<CategoryDto>(categoryUpdated);
            return categoryDto;
        }
    }
}
