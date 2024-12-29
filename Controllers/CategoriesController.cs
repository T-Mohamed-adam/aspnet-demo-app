using EmployeeAdminPortal.Models.Dtos;
using EmployeeAdminPortal.Models.Entities;
using EmployeeAdminPortal.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAdminPortal.Controllers
{
    // localhost:xxxx/api/categories
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService) 
        {
            _categoryService = categoryService; 
        }


        // Gel all categories
        [HttpGet]
        public async Task <IActionResult> GetAllCatgories()
        {   
           List<Category> categories = await _categoryService.GetAllCategories();

            return Ok(categories);
        }

        // Get specific category by its id
        [AllowAnonymous]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult?> GetCatgeoryById(Guid id) 
        {
            Category? category = await _categoryService.GetCatgeoryById(id);
            if (category is null) 
            {
                return NotFound("Category not found");
            }
            return Ok(category);
        }

        // Add new category 
        [HttpPost]
        public async Task<IActionResult?> AddCategory([FromForm] AddCategoryDto addCategoryDto) 
        {
            try
            {
                Category? category = await _categoryService.AddCategory(addCategoryDto);
                return Ok(category);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        // Update specific category by its id
        [HttpPut("{id:guid}")]
        public async Task<IActionResult?> UpdateCategory(Guid id, UpdateCategoryDto updateCategoryDto) 
        {
            try
            {
                Category? category = await _categoryService.UpdateCategory(id, updateCategoryDto);

                if (category is null)
                {
                    return NotFound("Category not found");
                }

                return Ok(category);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
           
        }

        // Delete specific catgory by its id
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult?> DeleteCategory(Guid id) 
        {
            Category? category = await _categoryService.DeleteCategory(id);
            if (category is null) 
            {
                return NotFound("Category not found");
            }

            return Ok("Category deleted successfully");
        }

    }
}
