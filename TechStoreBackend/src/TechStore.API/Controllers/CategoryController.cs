using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TechStore.Application.Interfaces.Services;
using TechStore.Application.Models.Category;
using TechStore.Domain.Entities.SubcategoryAggregate;

namespace TechStore.API.Controllers

{
    [Route("api/[controller]")]
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

        [HttpGet]
        public async Task<IEnumerable<CategoryReadModel>> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
         
            return _mapper.Map<IEnumerable<CategoryReadModel>>(categories);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryReadModel>> GetCategoryById(int id)
        {
            if (id < 1)
                return BadRequest();

            var category = await _categoryService.GetCategoryByIdAsync(id);

            if (category == null)
                return NotFound();

            return Ok(_mapper.Map<CategoryReadModel>(category));
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryCreateModel category)
        {
            if (category == null)
                return BadRequest();

            await _categoryService.CreateCategory(category);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] CategoryUpdateModel category)
        {
            if (category == null)
                return BadRequest();

            await _categoryService.UpdateCategory(category);

            return Ok();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1)
                return BadRequest();

            await _categoryService.DeleteCategory(id);

            return Ok();
        }
    }
}
