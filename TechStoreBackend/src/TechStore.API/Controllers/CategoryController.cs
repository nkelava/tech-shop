using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TechStore.Application.Interfaces.Services;
using TechStore.Application.Models.Category;
using TechStore.Application.Services;


namespace TechStore.API.Controllers

{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        public readonly ICategoryService _categoryService;
        public readonly IMapper _mapper;
        public readonly ILogger<CategoryController> _logger;

        public CategoryController(ICategoryService categoryService, IMapper mapper, ILogger<CategoryController> logger)
        {
            _categoryService = categoryService;
            _mapper = mapper;
            _logger = logger;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryCreateModel category)
        {
            if (category is null)
                return BadRequest();

            await _categoryService.CreateAsync(category);

            return Ok(category);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CategoryUpdateModel category)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var updatedCategory = await _categoryService.UpdateAsync(id, category);

                if (updatedCategory == null)
                {
                    _logger.LogWarning("Category with ID {id} was not found.", id);
                    return NotFound("Category not found.");
                }

                _logger.LogInformation("Category {ID} updated with name {Name}% and slug {Slug}.", id, category.Name, category.Slug);
                return Ok(updatedCategory);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the category.");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request.");
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1)
                return BadRequest();

            await _categoryService.DeleteAsync(id);

            return Ok(id);
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
                return BadRequest();

            try {
                var category = await _categoryService.GetCategoryBySlugAsync(slug);
                return (category is null) ? NotFound() : Ok(category);
            } catch {
                return BadRequest();
            }
        }

        [HttpGet]
        public async Task<IEnumerable<CategoryReadModel>> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return categories;
        }

        [HttpGet("{slug}/subcategories")]
        public async Task<IActionResult> GetCategoryWithSubcategories(string slug)
        {
            if (string.IsNullOrWhiteSpace(slug))
                return BadRequest();

            try {
                var category = await _categoryService.GetCategoryWithSubcategoriesAsync(slug);
                return (category is null) ? NotFound() : Ok(category);
            } catch {
                return BadRequest();
            }
        }
    }
}
