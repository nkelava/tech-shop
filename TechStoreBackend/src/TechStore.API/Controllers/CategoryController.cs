using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechStore.Application.Interfaces.Services;
using TechStore.Application.Models.Category;


namespace TechStore.API.Controllers

{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        public readonly ICategoryService _categoryService;
        public readonly IMapper _mapper;

        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CategoryCreateModel category)
        {
            if (category == null)
                return BadRequest();

            await _categoryService.AddAsync(category);

            return Ok(category);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] CategoryUpdateModel category)
        {
            if (category == null)
                return BadRequest();

            await _categoryService.UpdateAsync(category);

            return Ok(category);
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

        [HttpGet]
        public async Task<IList<CategoryReadModel>> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();

            return categories;
        }

        [HttpGet("{slug}")]
        public async Task<ActionResult<CategoryReadModel>> GetCategoryBySlug(string slug)
        {
            if (string.IsNullOrWhiteSpace(slug))
                return BadRequest();

            try
            {
                var category = await _categoryService.GetCategoryBySlugAsync(slug);

                if (category == null)
                    return NotFound();

                return Ok(category);
            } catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{slug}/subcategories")]
        public async Task<IActionResult> GetCategoryWithSubcategories(string slug)
        {
            if (string.IsNullOrWhiteSpace(slug))
                return BadRequest();

            try
            {
                var category = await _categoryService.GetCategoryWithSubcategoriesAsync(slug);

                if (category == null)
                    return NotFound();

                return Ok(category);
            } catch
            {
                return BadRequest();
            }
        }
    }
}
