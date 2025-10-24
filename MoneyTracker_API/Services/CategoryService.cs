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

        public async Task<CategoryDto> CreateCategory(CategoryCreateDto categoryCreateDto)
        {
            var category = _mapper.Map<Category>(categoryCreateDto);
            Category categoryFromCreate = await _repo.Create(category);
            CategoryDto dto = _mapper.Map<CategoryDto>(categoryFromCreate);
            return dto;
        }

        public async Task<List<CategoryDto>?> GetAllCategories() 
        {
            var list = await _repo.GetAll();
            if(list == null)
            {
                return null;
            } 
            List<CategoryDto> categories = _mapper.Map<List<CategoryDto>>(list);
            return categories;
        }   

        /*public async Task<List<CategoryDto>> GetAllSubCategoriesForParentCat(int ParentCatId)
        {
           List<Category> subCategories =
                await _repo.GetAll(c => c.ParentCategoryId == ParentCatId);
            List<CategoryDto> sunCategoriesDto = _mapper.Map<List<CategoryDto>>(subCategories);
            return sunCategoriesDto;
        }*/

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

        public async Task<List<CategoryDto>> GetParentCategories()
        {
            var List = await _repo.GetParentCategories();
            List<CategoryDto> parentCategories = _mapper.Map<List<CategoryDto>>(List);
            return parentCategories;
        }

        public async Task<List<CategoryDto>> GetParentCategoriesByType(SD.CategoryType type)
        {
            /*var parentCategories = await GetParentCategories();
            var parentCategoriesFilteredByType =
                parentCategories.Where(c => c.Type == type).ToList();*/
            var parentCategoriesFilteredByType = await _repo.GetParentCategoriesByType(type);
            var parentCategoriesFilteredByTypeDto = 
                 _mapper.Map<List<CategoryDto>>(parentCategoriesFilteredByType);
            return parentCategoriesFilteredByTypeDto;
        }

        public async Task<List<CategoryDto>> GetSubCategoriesByParentId(int parentCategoryId)
        {
            List<Category> subCategories =
                await _repo.GetAll(c => c.ParentCategoryId == parentCategoryId);
            List<CategoryDto> sunCategoriesDto = _mapper.Map<List<CategoryDto>>(subCategories);
            return sunCategoriesDto;
        }

/*        public Task<CategoryDto> UpdateCategory(CategoryUpdateDto categoryUpdateDto)
        {
            throw new NotImplementedException();
        }*/

        public async Task<CategoryDto?> UpdateCategory(int id, CategoryUpdateDto categoryUpdateDto)
        {
            Category? category = await _repo.Get(c => c.Id == id);
            if(category != null)
            {
                _mapper.Map(categoryUpdateDto, category);
              //  Category CategoryToUpdate = _mapper.Map<Category>(categoryUpdateDto);
                Category? categoryUpdated = await _repo.Update(category);
                CategoryDto categoryDto = _mapper.Map<CategoryDto>(categoryUpdated);
                return categoryDto;
            }
            return null;
        }

        public async Task<bool> DeleteCategory(int id)
        {
            Category? category = await _repo.Get(c => c.Id == id);
            return await _repo.Delete(category);
        }
    }
}
