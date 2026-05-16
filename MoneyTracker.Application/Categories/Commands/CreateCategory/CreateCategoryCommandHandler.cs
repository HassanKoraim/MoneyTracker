using AutoMapper;
using MediatR;
using MoneyTracker.Application.DTOs.CategoryDTOs;
using MoneyTracker.Application.RepositoryContracts;
using MoneyTracker.Domain.Entities;
using MoneyTracker.Domain.Enums;

namespace MoneyTracker.Application.Categories.Commands.CreateCategory
{
    public sealed class CreateCategoryCommandHandler(ICategoriesRepository _repo, IMapper _mapper) : IRequestHandler<CreateCategoryCommand, CategoryDto>
    {
        public async Task<CategoryDto> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            bool IsExists = await _repo.CategoryExists(request.CategoryCreateDto.Name, request.CategoryCreateDto.Type);
            if(IsExists)
            {
                throw new ArgumentException("Category already exists");
            }
            if(request.CategoryCreateDto.ParentCategoryId != null)
            {
                Category? categorydb = await _repo.Get(c => c.Id == request.CategoryCreateDto.ParentCategoryId);
                if (categorydb == null)
                {
                    throw new KeyNotFoundException("Parent category does not exist");
                }
                request.CategoryCreateDto.Type = categorydb.Type;
            }
            Category category = _mapper.Map<Category>(request.CategoryCreateDto);
            await _repo.Create(category);
            return _mapper.Map<CategoryDto>(category);
        }
    }
}
