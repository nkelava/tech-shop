using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TechStore.Application.Interfaces.Services;
using TechStore.Application.Models.Subcategory;


namespace TechStore.API.Controllers
{
    [Route("api/[controller]")]
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

        [HttpGet]
        public async Task<IEnumerable<SubcategoryReadModel>> GetAllSubcategories()
        {
            var subcategories = await _subcategoryService.GetAllSubcategoriesAsync();

            return subcategories;
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


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SubcategoryCreateModel subcategory)
        {
            if (subcategory == null)
                return BadRequest();

            await _subcategoryService.CreateSubcategory(subcategory);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] SubcategoryUpdateModel subcategory)
        {
            if (subcategory == null)
                return BadRequest();

            await _subcategoryService.UpdateSubcategory(subcategory);

            return Ok();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1)
                return BadRequest();

            await _subcategoryService.DeleteSubcategory(id);

            return Ok();
        }
    }
}
