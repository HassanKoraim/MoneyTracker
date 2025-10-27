using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using MoneyTracker_API.Data;
using MoneyTracker_API.DTOs;
using MoneyTracker_API.Models;
using MoneyTracker_API.RepositoryContracts;
using MoneyTracker_API.ServiceContracts;
using MoneyTracker_Utility;
using System.Linq.Expressions;

namespace MoneyTracker_API.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoriesRepository _repo;
        private readonly IMapper _mapper;
        public CategoryService(ICategoriesRepository repo, IMapper mapper):base()
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<List<CategoryDto>> GetAllCategories() 
        {
            var list = await _repo.GetAll();
            if(list == null || !list.Any())
            {
                return new List<CategoryDto>();
            } 
            List<CategoryDto> categories =
                _mapper.Map<List<CategoryDto>>(list);
            return categories;
        }   
        /// <summary>
        /// To Return Category Type, Parent Category or SubCategory 
        /// </summary>
        /// <param name="id">This give id Parameter</param>
        /// <returns>Return Category Type, Parent Category or SubCategory</returns>
        public async Task<CategoryDto?> GetCategory(int id)
        {
            if(id <= 0 )
            {
                return null;
            }
            List<Category> categories = await _repo.GetAll(includeProp: "ParentCategory,SubCategories,Incomes,Expenses");
            if(categories == null)
            {
                return null;
            }
            Category ? category = await _repo.Get(c=> c.Id == id, includeProp: "ParentCategory,SubCategories,Incomes,Expenses");
            if(category == null)
            {
                return null;
            }
            CategoryDto categoryDto = _mapper.Map<CategoryDto>(category);
            return categoryDto;
        }

        /// <summary>
        /// To Return All Parent Categories Type
        /// </summary>
        /// <returns>Returns All Parent Categories</returns>
        public async Task<List<CategoryDto>> GetParentCategories()
        {
            var categories = await _repo.GetParentCategories();
            if (categories == null || !categories.Any())
            {
                return new List<CategoryDto>();
            }
            var parentCategories = _mapper.Map<List<CategoryDto>>(categories);
            return parentCategories;
        }

        public async Task<List<CategoryDto>> GetParentCategoriesByType(SD.CategoryType? type)
        {
            if(type == null)
            {
                return await GetParentCategories();
            }
            var parentCategories =
                await _repo.GetParentCategoriesByType(type);
            if (parentCategories == null || !parentCategories.Any())
            {
                return new List<CategoryDto>();  // Empty List
            }
            var parentCategoriesDto = 
                 _mapper.Map<List<CategoryDto>>(parentCategories);
            return parentCategoriesDto;
        }

        public async Task<List<CategoryDto>> GetSubCategoriesByParentId(int parentCategoryId)
        {
            List<Category> subCategories =
                await _repo.GetAll(c => c.ParentCategoryId == parentCategoryId);
            if(subCategories == null || !subCategories.Any())
            {
                return new List<CategoryDto>();
            }
            List<CategoryDto> subCategoriesDto =
                _mapper.Map<List<CategoryDto>>(subCategories);
            return subCategoriesDto;
        }
        /// <summary>
        /// Create a Perant and Sub category Type
        /// </summary>
        /// <param name="categoryCreateDto">give categoryCreateDto class</param>
        /// <returns>returns CategoryDto Class</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<CategoryDto> CreateCategory(CategoryCreateDto categoryCreateDto)
        {
            if (categoryCreateDto == null)
            {
                throw new ArgumentNullException(nameof(categoryCreateDto));
            }
            // check for null Category Name
            if (string.IsNullOrWhiteSpace(categoryCreateDto.Name))
            {
                throw new ArgumentException("Category name cannot be empty", nameof(categoryCreateDto.Name));
            }
            // Check for duplicate category name
            bool categoryExists = await _repo.CategoryExists(categoryCreateDto.Name, categoryCreateDto.Type);
            if (categoryExists)
            {
                throw new InvalidOperationException($"A category with name '{categoryCreateDto.Name}' already exists for type '{categoryCreateDto.Type}'");
            }
            if(categoryCreateDto.ParentCategoryId <= 0)
            {
                throw new ArgumentException("Parent category Id Cannot be Equal or less than Zero");
            }
            // Validate parent category exists if provided
            if (categoryCreateDto.ParentCategoryId.HasValue)
            {
                var parentCategory =
                    await _repo.Get(
                        c => c.Id == categoryCreateDto.ParentCategoryId 
                        && c.ParentCategoryId == null);  //check for exists the Category in db and didn't subcategory
                if (parentCategory == null)
                {
                    throw new ArgumentException("Parent category not found", nameof(categoryCreateDto.ParentCategoryId));
                }
                if(categoryCreateDto.SubCategories.Any())
                {
                    throw new ArgumentException("Sub category Cann't be a Parent For categories ");
                }
            }
            var category = _mapper.Map<Category>(categoryCreateDto);
            Category categoryFromCreate = await _repo.Create(category);
            CategoryDto dto = _mapper.Map<CategoryDto>(categoryFromCreate);
            return dto;
        }
        public async Task<CategoryDto> UpdateCategory(int id, CategoryUpdateDto categoryUpdateDto)
        {
            if (categoryUpdateDto == null)
            {
                throw new ArgumentNullException(nameof(categoryUpdateDto));
            }
            if (string.IsNullOrWhiteSpace(categoryUpdateDto.Name))
            {
                throw new ArgumentException("Category name cannot be empty", nameof(categoryUpdateDto.Name));
            }
            
            if (categoryUpdateDto.ParentCategoryId <= 0)
            {
                throw new ArgumentException("Parent category Id Cannot be Equal or less than Zero");
            }
            // Validate parent category exists if provided
            if (categoryUpdateDto.ParentCategoryId.HasValue)
            {
                var parentCategory =
                    await _repo.Get(
                        c => c.Id == categoryUpdateDto.ParentCategoryId
                        && c.ParentCategoryId == null);  //check for exists the Category in db and didn't subcategory
                if (parentCategory == null)
                {
                    throw new ArgumentException("Parent category not found", nameof(categoryUpdateDto.ParentCategoryId));
                }
            }
            Category category = await _repo.Get(c => c.Id == id);
            _mapper.Map(categoryUpdateDto, category);
            Category? categoryUpdated = await _repo.Update(category);
            CategoryDto categoryDto = _mapper.Map<CategoryDto>(categoryUpdated);
            return categoryDto;
        }

        public async Task<bool> DeleteCategory(int id)
        {
            if(id <= 0)
            {
                return false;
            }
            Category? category = await _repo.Get(c => c.Id == id, includeProp: "SubCategories");
            if(category == null)
            {
                return false;
            }
            if(category.SubCategories != null && category.SubCategories.Any())
            {
                await _repo.DeleteRange(category.SubCategories);
               
            }
            return await _repo.Delete(category);
        }
    }
}
