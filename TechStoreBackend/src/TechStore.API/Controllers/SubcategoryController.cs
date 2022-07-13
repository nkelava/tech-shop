using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TechStore.Application.Interfaces.Services;
using TechStore.Application.Models.Subcategory;


namespace TechStore.API.Controllers
{
    [Route("api/subcategories")]
    [ApiController]
    public class SubcategoryController : ControllerBase
    {
        public readonly ISubcategoryService _subcategoryService;
        public readonly IMapper _mapper;

        public SubcategoryController(ISubcategoryService subcategoryService, IMapper mapper)
        {
            _subcategoryService = subcategoryService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] SubcategoryCreateModel subcategory)
        {
            if (subcategory == null)
                return BadRequest();

            await _subcategoryService.AddAsync(subcategory);

            return Ok(subcategory);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] SubcategoryUpdateModel subcategory)
        {
            if (subcategory == null)
                return BadRequest();

            await _subcategoryService.UpdateAsync(subcategory);

            return Ok(subcategory);
        }

        [HttpGet]
        public async Task<IEnumerable<SubcategoryReadModel>> GetAllSubcategories()
        {
            var subcategories = await _subcategoryService.GetAllSubcategoriesAsync();

            return subcategories;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1)
                return BadRequest();

            await _subcategoryService.DeleteAsync(id);

            return Ok(id);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubcategoryById(int id)
        {
            if (id < 1)
                return BadRequest();

            var subcategory = await _subcategoryService.GetSubcategoryByIdAsync(id);

            if (subcategory == null)
                return NotFound();

            return Ok(subcategory);
        }
    }
}
