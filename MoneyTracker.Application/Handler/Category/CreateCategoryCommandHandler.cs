using AutoMapper;
using MediatR;
using MoneyTracker.Application.Commands.Category;
using MoneyTracker.Application.DTOs;
using MoneyTracker.Application.RepositoryContracts;


namespace MoneyTracker.Application.Handler.Category
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CategoryDto>
    {
        private readonly ICategoriesRepository _repo;
        private readonly IMapper _mapper;
        public CreateCategoryCommandHandler(ICategoriesRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        /// <summary>
        /// Create a Perant and Sub category Type
        /// </summary>
        /// <param name="categoryCreateDto">give categoryCreateDto class</param>
        /// <returns>returns CategoryDto Class</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<CategoryDto> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            if (request.categoryCreateDto == null)
            {
                throw new ArgumentNullException(nameof(request.categoryCreateDto));
            }
            // check for null Category Name
            if (string.IsNullOrWhiteSpace(request.categoryCreateDto.Name))
            {
                throw new ArgumentException("Category name cannot be empty", nameof(request.categoryCreateDto.Name));
            }
            // Check for duplicate category name
            bool categoryExists = await _repo.CategoryExists(request.categoryCreateDto.Name, request.categoryCreateDto.Type);
            if (categoryExists)
            {
                throw new InvalidOperationException($"A category with name '{request.categoryCreateDto.Name}' already exists for type '{request.categoryCreateDto.Type}'");
            }
            if (request.categoryCreateDto.ParentCategoryId <= 0)
            {
                throw new ArgumentException("Parent category Id Cannot be Equal or less than Zero");
            }
            // Validate parent category exists if provided
            if (request.categoryCreateDto.ParentCategoryId.HasValue)
            {
                var parentCategory =
                    await _repo.Get(
                        c => c.Id == request.categoryCreateDto.ParentCategoryId
                        && c.ParentCategoryId == null);  //check for exists the Category in db and didn't subcategory
                if (parentCategory == null)
                {
                    throw new ArgumentException("Parent category not found", nameof(request.categoryCreateDto.ParentCategoryId));
                }
                if (request.categoryCreateDto.SubCategories.Any())
                {
                    throw new ArgumentException("Sub category Cann't be a Parent For A sub category");
                }
            }
            var category = _mapper.Map<Domain.Entities.Category>(request.categoryCreateDto);
            Domain.Entities.Category categoryFromCreate = await _repo.Create(category);
            CategoryDto dto = _mapper.Map<CategoryDto>(categoryFromCreate);
            return dto;
        }
    }
}
