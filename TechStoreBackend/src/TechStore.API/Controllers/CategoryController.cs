using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TechStore.Application.Interfaces.Services;
using TechStore.Application.Models.Category;
using TechStore.Domain.Entities.SubcategoryAggregate;

namespace TechStore.API.Controllers

{
    [Route("api/category")]
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

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CategoryCreateModel category)
        {
            if (category == null)
                return BadRequest();

            await _categoryService.AddAsync(category);

            return Ok(category);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] CategoryUpdateModel category)
        {
            if (category == null)
                return BadRequest();

            await _categoryService.UpdateAsync(category);

            return Ok(category);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1)
                return BadRequest();

            await _categoryService.DeleteAsync(id);

            return Ok(id);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryReadModel>> GetCategoryById(int id)
        {
            if (id < 1)
                return BadRequest();

            var category = await _categoryService.GetCategoryByIdAsync(id);

            if (category == null)
                return NotFound();

            return Ok(category);
        }

        [HttpGet("{id}/subcategories")]
        public async Task<IActionResult> GetCategoryWithSubcategories(int id)
        {
            var category = await _categoryService.GetCategoryWithSubcategoriesAsync(id);

            return Ok(category);
        }

        [HttpGet("/api/categories")]
        public async Task<IList<CategoryReadModel>> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();

            return categories;
        }
    }
}
