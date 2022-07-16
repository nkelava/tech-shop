using Microsoft.AspNetCore.Mvc;
using TechStore.Application.Interfaces.Services;
using TechStore.Application.Models.Property;


namespace TechStore.API.Controllers
{
    [Route("api/properties")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        public readonly IPropertyService _propertyService;

        public PropertyController(IPropertyService propertyService)
        {
            _propertyService = propertyService;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] PropertyCreateModel property)
        {
            if (property == null)
                return BadRequest();

            await _propertyService.AddAsync(property);

            return Ok(property);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1)
                return BadRequest();

            await _propertyService.DeleteAsync(id);

            return Ok(id);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] PropertyUpdateModel property)
        {
            if (property == null)
                return BadRequest();

            await _propertyService.UpdateAsync(property);

            return Ok(property);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetPropertyById(int id)
        {
            if (id < 1)
                return BadRequest();

            var property = await _propertyService.GetPropertyByIdAsync(id);

            if (property == null)
                return NotFound();

            return Ok(property);
        }

        [HttpGet]
        public async Task<IActionResult> GetPropertyByName(string name)
        {
            if (name == null || name.Length < 1)
                return BadRequest();

            var property = await _propertyService.GetPropertyByNameAsync(name);

            if (property == null)
                return NotFound();

            return Ok(property);
        }

    }
}
