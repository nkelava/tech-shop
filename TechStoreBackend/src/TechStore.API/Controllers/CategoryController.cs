using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TechStore.Application.Interfaces.Services;
using TechStore.Application.Models.Category;


namespace TechStore.API.Controllers

{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ICategoryService categoryService, ILogger<CategoryController> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CategoryCreateModel category)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var newCategory = await _categoryService.CreateAsync(category);

                if (newCategory == null)
                    return BadRequest("Category already exists.");

                _logger.LogInformation("Category created with name {Name} and slug {Slug}.", category.Name, category.Slug);
                return Ok(newCategory);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the category.");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] CategoryUpdateModel category)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id < 1)
            {
                _logger.LogWarning("Invalid category ID {Id}.", id);
                return BadRequest("Invalid category ID.");
            }

            try
            {
                var updatedCategory = await _categoryService.UpdateAsync(id, category);

                if (updatedCategory == null)
                {
                    _logger.LogWarning("Category with ID {id} was not found.", id);
                    return NotFound("Category not found.");
                }

                _logger.LogInformation("Category {ID} updated with name {Name} and slug {Slug}.", id, category.Name, category.Slug);
                return Ok(updatedCategory);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the category.");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1)
            {
                _logger.LogWarning("Invalid category ID {Id}.", id);
                return BadRequest("Invalid category ID.");
            }

            try
            {
                int? deletedCategoryId = await _categoryService.DeleteAsync(id);

                if (deletedCategoryId == null)
                {
                    _logger.LogWarning("Category with ID {Id} was not found.", id);
                    return NotFound("Category not found.");
                }

                _logger.LogInformation("Category with ID {Id} was deleted successfully.", id);
                return Ok(deletedCategoryId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the category with ID {Id}.", id);
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CategoryReadModel>> GetById(int id)
        {
            _logger.LogInformation("Received request to fetch category with ID {Id}.", id);

            if (id < 1)
            {
                _logger.LogWarning("Invalid category ID {Id}.", id);
                return BadRequest("Invalid category ID.");
            }

            try
            {
                var category = await _categoryService.GetByIdAsync(id);

                if (category == null)
                {
                    _logger.LogWarning("Category with ID {Id} not found.", id);
                    return NotFound("Category not found.");
                }

                _logger.LogInformation("Category with ID {Id} fetched successfully.", id);
                return Ok(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching category.");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpGet("{slug}")]
        public async Task<ActionResult<CategoryReadModel>> GetCategoryBySlug(string slug)
        {
            if (string.IsNullOrWhiteSpace(slug))
            {
                _logger.LogWarning("Invalid category slug {Slug}.", slug);
                return BadRequest("Invalid category slug.");
            }

            try
            {
                var category = await _categoryService.GetBySlugAsync(slug);

                if (category == null)
                {
                    _logger.LogWarning("Category with slug {Slug} not found.", slug);
                    return NotFound("Category not found.");
                }

                _logger.LogInformation("Category with slug {Slug} fetched successfully.", slug);
                return Ok(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching category.");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpGet("{slug}/subcategories")]
        public async Task<IActionResult> GetCategoryWithSubcategories(string slug)
        {
            if (string.IsNullOrWhiteSpace(slug))
            {
                _logger.LogWarning("Invalid category slug {Slug}.", slug);
                return BadRequest("Invalid category slug.");
            }

            try
            {
                var category = await _categoryService.GetWithSubcategoriesAsync(slug);
                
                if (category == null)
                {
                    _logger.LogWarning("Category with slug {Slug} not found.", slug);
                    return NotFound("Category not found.");
                }

                _logger.LogInformation("Category with slug {Slug} fetched successfully.", slug);
                return Ok(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching category.");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            _logger.LogInformation("Fetching all categories.");

            try
            {
                var categories = await _categoryService.GetAllAsync();

                _logger.LogInformation("Successfully fetched all categories.");
                return Ok(categories);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching categories.");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request.");
            }
        }
    }
}
