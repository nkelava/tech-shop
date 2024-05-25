using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SubcategoryCreateModel subcategory)
        {
            if (subcategory is null)
                return BadRequest();

            await _subcategoryService.CreateAsync(subcategory);

            return Ok(subcategory);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] SubcategoryUpdateModel subcategory)
        {
            if (subcategory is null)
                return BadRequest();

            await _subcategoryService.UpdateAsync(subcategory);

            return Ok(subcategory);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1)
                return BadRequest();

            await _subcategoryService.DeleteAsync(id);

            return Ok(id);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetSubcategoryById(int id)
        {
            if (id < 1)
                return BadRequest();

            var subcategory = await _subcategoryService.GetSubcategoryByIdAsync(id);

            return (subcategory is null) ? NotFound() : Ok(subcategory);
        }


        [HttpGet("{slug}")]
        public async Task<IActionResult> GetSubcategoryBySlug(string slug)
        {
            if (string.IsNullOrWhiteSpace(slug))
                return BadRequest();

            var subcategory = await _subcategoryService.GetSubcategoryBySlugAsync(slug);

            return (subcategory is null) ? NotFound() : Ok(subcategory);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSubcategories()
        {
            var subcategories = await _subcategoryService.GetAllSubcategoriesAsync();

            return Ok(subcategories);
        }
    }
}
