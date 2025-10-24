using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MoneyTracker_API.DTOs;
using MoneyTracker_API.ServiceContracts;
using MoneyTracker_Utility;

namespace MoneyTracker_API.Controllers
{
    [Route("api/CategoryApi")]
    [ApiController]
    public class CategoryAPIController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryAPIController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
/*        public IActionResult Index()
        {
            return View();
        }*/
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _categoryService.GetAllCategories();
            return Ok(categories);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            CategoryDto? categoryDto = await _categoryService.GetCategory(id);
            return Ok(categoryDto);
        }
        [HttpGet("parent/{parentId:int}")]
        public async Task<IActionResult> GetSubCategoriesForParent(int parentId)
        {
            var subCategories = await _categoryService.GetSubCategoriesByParentId(parentId);
            return Ok(subCategories);
        }
        [HttpGet("parents")]
        public async Task<IActionResult> GetParentCategories()
        {
            var parentCategories = await _categoryService.GetParentCategories();
            return Ok(parentCategories);
        }
        [HttpGet("Income")]
        public async Task<IActionResult> GetIncomeParentCategories()
        {
            var parentCatigories = await _categoryService.GetParentCategoriesByType(SD.CategoryType.Income);
            return Ok(parentCatigories);
        }
        [HttpGet("Expense")]
        public async Task<IActionResult> GetExpenseParentCategories()
        {
            var parentCatigories = await _categoryService.GetParentCategoriesByType(SD.CategoryType.Expense);
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

                var category = await _categoryService.CreateCategory(dto);
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
            CategoryDto? categoryDto = await _categoryService.UpdateCategory(id, dto);
            if (categoryDto == null) return BadRequest();
            return Ok(categoryDto); 
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            bool IsSuccess = await _categoryService.DeleteCategory(id);
            if(IsSuccess) return Ok();
            else return BadRequest();
        }

    }
}
