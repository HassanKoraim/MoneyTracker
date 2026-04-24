using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MoneyTracker.Application.DTOs;
using MoneyTracker.Application.Queries.Category;
using MoneyTracker.Application.Commands.Category;
using MoneyTracker.Application.ServiceContracts;
using MoneyTracker.Domain.Enums;

namespace MoneyTracker.API.Controllers
{
    [Route("api/CategoryApi")]
    [ApiController]
    public class CategoryAPIController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IMediator _mediator;
        public CategoryAPIController(ICategoryService categoryService, IMediator mediator)
        {
            _categoryService = categoryService;
            _mediator = mediator;
        }
/*        public IActionResult Index()
        {
            return View();
        }*/
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
        public async Task<IActionResult> GetSubCategoriesForParent(int parentId)
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
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryCreateDto dto)
        {
            /* CategoryDto CreatedCategory = await _categoryService.CreateCategory(dto);
             return Ok(CreatedCategory);*/
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var category = await _mediator.Send(new CreateCategoryCommand(dto));
                //   return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category);
                return Ok(category);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
              //  _logger.LogError(ex, "Error creating category");
                return StatusCode(500, "An error occurred while creating the category");
            }
        }

        [HttpPut("{id:int}")]
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
