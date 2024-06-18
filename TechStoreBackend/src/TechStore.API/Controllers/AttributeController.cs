using Microsoft.AspNetCore.Mvc;
using TechStore.Application.Interfaces.Services;
using TechStore.Application.Models.Attribute;


namespace TechStore.API.Controllers
{
    [Route("api/attributes")]
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

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1)
                return BadRequest();

            int deletedAttributeId = await _attributeService.DeleteAsync(id);
            return deletedAttributeId < 1 ? NotFound() : Ok(id);

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

            var attibute = await _attributeService.GetByIdAsync(id);

            return (attibute is null) ? NotFound() : Ok(attibute);
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetAttributeByName(string name)
        {
            if (name is null || name.Length < 1)
                return BadRequest();

            var attibute = await _attributeService.GetByNameAsync(name);

            return (attibute is null) ? NotFound() : Ok(attibute);
        }


        [HttpGet]
        public async Task<IEnumerable<AttributeReadModel>> GetAllAttributes()
        {
            var attributes = await _attributeService.GetAllAsync();

            return attributes;
        }
    }
}
