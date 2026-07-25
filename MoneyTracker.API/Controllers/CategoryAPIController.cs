using MediatR;
using Microsoft.AspNetCore.Mvc;
using MoneyTracker.Application.Categories.Commands.DeleteCategory;
using MoneyTracker.Application.Categories.Commands.UpdateCategory;
using MoneyTracker.Application.Categories.Queries.GetAllCategories;
using MoneyTracker.Application.Categories.Queries.GetCategoryById;
using MoneyTracker.Application.Categories.Queries.GetParentCategories;
using MoneyTracker.Application.Categories.Queries.GetParentCategoriesByType;
using MoneyTracker.Application.Categories.Queries.GetSubCategoriesByParentId;
using MoneyTracker.Application.DTOs.CategoryDTOs;
using MoneyTracker.Domain.Enums;
using MoneyTracker.Application.Categories.Commands.CreateCategory;
using Microsoft.AspNetCore.Authorization;

namespace MoneyTracker.API.Controllers
{
    [Route("api/CategoryApi")]
    [ApiController]
    [Authorize(Roles = nameof(SD.RoleType.Admin))]
    public class CategoryAPIController : Controller
    {
        private readonly IMediator _mediator;
        public CategoryAPIController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            //var categories = await _categoryService.GetAllCategories();
            var categories = await _mediator.Send(new GetAllCategoriesQuery());
            
            return Ok(categories);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            CategoryDto? categoryDto = await _mediator.Send(new GetCategoryByIdQuery(id));
            return Ok(categoryDto);
        }
        [HttpGet("parent/{parentId:int}")]
        public async Task<IActionResult> GetSubCategoriesForParent(int? parentId)
        {
            var subCategories = await _mediator.Send(new GetSubCategoriesByParentIdQuery(parentId));
            return Ok(subCategories);
        }
        [HttpGet("parents")]
        public async Task<IActionResult> GetParentCategories()
        {
            var parentCategories = await _mediator.Send(new GetParentCategoriesQuery());
            return Ok(parentCategories);
        }
        [HttpGet("Income")]
        public async Task<IActionResult> GetIncomeParentCategories()
        {
            var parentCatigories = await _mediator.Send(new GetParentCategoriesByTypeQuery(SD.CategoryType.Income));
            return Ok(parentCatigories);
        }
        [HttpGet("Expense")]
        public async Task<IActionResult> GetExpenseParentCategories()
        {
            var parentCatigories = await _mediator.Send(new GetParentCategoriesByTypeQuery(SD.CategoryType.Expense));
            return Ok(parentCatigories);
        }
        [HttpPost("CreateCategory")]
        public async Task<IActionResult> CreateCategory(CategoryCreateDto dto)
        {
            var category = await _mediator.Send(new CreateCategoryCommand(dto));
            return Ok(category);
        }
        [HttpPut("UpdateCategory/{id:int}")]
        public async Task<IActionResult> UpdateCategory(int id, CategoryUpdateDto dto)
        {
            CategoryDto? categoryDto = await _mediator.Send(new UpdateCategoryCommand(id, dto));
            if (categoryDto == null) return BadRequest();
            return Ok(categoryDto); 
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            bool IsSuccess = await _mediator.Send(new DeleteCategoryCommand(id));
            if(IsSuccess) return Ok();
            else return BadRequest();
        }

    }
}
