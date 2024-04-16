using Microsoft.AspNetCore.Mvc;
using TechStore.Application.Interfaces.Services;
using TechStore.Application.Models.Attribute;


namespace TechStore.API.Controllers
{
    [Route("api/attribute")]
    [ApiController]
    public class AttributeController : ControllerBase
    {
        public readonly IAttributeService _attributeService;

        public AttributeController(IAttributeService attributeService)
        {
            _attributeService = attributeService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AttributeCreateModel attibute)
        {
            if (attibute is null)
                return BadRequest();
            
            try {
                await _attributeService.CreateAsync(attibute);
                return Ok(attibute);
            } catch {
                return BadRequest();
            }

        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1)
                return BadRequest();

            try {
                await _attributeService.DeleteAsync(id);
                return Ok(id);
            } catch {
                return BadRequest();
            }

        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] AttributeUpdateModel attibute)
        {
            if (attibute is null)
                return BadRequest();

            try {
                await _attributeService.UpdateAsync(attibute);
                return Ok(attibute);
            }
            catch {
                return BadRequest();
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAttributeById(int id)
        {
            if (id < 1)
                return BadRequest();

            var attibute = await _attributeService.GetAttributeByIdAsync(id);

            return (attibute is null) ? NotFound() : Ok(attibute);
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetAttributeByName(string name)
        {
            if (name is null || name.Length < 1)
                return BadRequest();

            var attibute = await _attributeService.GetAttributeByNameAsync(name);

            return (attibute is null) ? NotFound() : Ok(attibute);
        }

        [HttpGet]
        public async Task<IEnumerable<AttributeReadModel>> GetAllAttributes()
        {
            var attributes = await _attributeService.GetAllAttributesAsync();

            return attributes;
        }
    }
}
