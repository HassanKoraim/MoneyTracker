using AutoMapper;
using MediatR;
using MoneyTracker.Application.DTOs.CategoryDTOs;
using MoneyTracker.Application.RepositoryContracts;
using MoneyTracker.Domain.Enums;

namespace MoneyTracker.Application.Categories.Commands.UpdateCategory
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
            
            if (request.categoryUpdateDto.ParentCategoryId.HasValue)
            {
                // Check for circular reference, parentId != id
                if (request.categoryUpdateDto.ParentCategoryId == request.id)
                {
                    throw new ArgumentException("A category cannot be its own parent", nameof(request.categoryUpdateDto.ParentCategoryId));
                }
                var parentCategoryInDb =
                    await _repo.Get(
                        c => c.Id == request.categoryUpdateDto.ParentCategoryId
                        && c.ParentCategoryId == null);  //check for exists the Category in db and didn't subcategory
                                                         
                if (parentCategoryInDb == null)
                {
                    throw new KeyNotFoundException("Parent category not found!");
                }
                var subCategoriesInDb =
                   await _repo.GetAll(
                       c => c.ParentCategoryId == request.id);
                if(subCategoriesInDb != null && subCategoriesInDb.Any())
                {
                    throw new ArgumentException("Cannot assign a parent category to a category that has subcategories", nameof(request.categoryUpdateDto.ParentCategoryId));
                }
              
                request.categoryUpdateDto.Type = parentCategoryInDb.Type; // Set the type to match the parent category

            } else
            {
                var subCategories =
                  await _repo.GetAll(
                      c => c.ParentCategoryId == request.id);
                if (subCategories != null && subCategories.Any())
                {
                    foreach ( var subCategory in subCategories)
                    {
                        subCategory.Type = request.categoryUpdateDto.Type; // Update the type of subcategories to match the new type of the parent category
                        await _repo.Update(subCategory);
                    }
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
