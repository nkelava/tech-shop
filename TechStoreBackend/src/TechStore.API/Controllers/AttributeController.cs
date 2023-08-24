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
        public async Task<IActionResult> Add([FromBody] AttributeCreateModel attibute)
        {
            if (attibute == null)
                return BadRequest();

            await _attributeService.AddAsync(attibute);

            return Ok(attibute);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1)
                return BadRequest();

            await _attributeService.DeleteAsync(id);

            return Ok(id);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] AttributeUpdateModel attibute)
        {
            if (attibute == null)
                return BadRequest();

            await _attributeService.UpdateAsync(attibute);

            return Ok(attibute);
        }

        [HttpGet]
        public async Task<IList<AttributeReadModel>> GetAllAttributes()
        {
            var attributes = await _attributeService.GetAllAttributesAsync();

            return attributes;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAttributeById(int id)
        {
            if (id < 1)
                return BadRequest();

            var attibute = await _attributeService.GetAttributeByIdAsync(id);

            if (attibute == null)
                return NotFound();

            return Ok(attibute);
        }

        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetAttributeByName(string name)
        {
            if (name == null || name.Length < 1)
                return BadRequest();

            var attibute = await _attributeService.GetAttributeByNameAsync(name);

            if (attibute == null)
                return NotFound();

            return Ok(attibute);
        }

    }
}
