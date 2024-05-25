using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechStore.Application.Interfaces.Services;
using TechStore.Application.Models.AttributeValue;


namespace TechStore.API.Controllers
{
    [Route("api/attribute-values")]
    [Authorize]
    [ApiController]
    public class AttributeValuesController : ControllerBase
    {
        public readonly IAttributeValueService _attributeValueService;

        public AttributeValuesController(IAttributeValueService attributeValueService)
        {
            _attributeValueService = attributeValueService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AttributeValueCreateModel attributeValue)
        {
            if (attributeValue is null)
                return BadRequest();

            try
            {
                await _attributeValueService.CreateAsync(attributeValue);
                return Ok(attributeValue);
            }
            catch
            {
                return BadRequest();
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1)
                return BadRequest();

            var deletedAttributeValueId = await _attributeValueService.DeleteAsync(id);
            return deletedAttributeValueId < 1 ? NotFound() : Ok(deletedAttributeValueId);

        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] AttributeValueUpdateModel attibute)
        {
            if (attibute is null)
                return BadRequest();

            try
            {
                await _attributeValueService.UpdateAsync(attibute);
                return Ok(attibute);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id < 1)
                return BadRequest();

            var attibuteValue = await _attributeValueService.GetByIdAsync(id);

            return (attibuteValue is null) ? NotFound() : Ok(attibuteValue);
        }

        [HttpGet]
        public async Task<IEnumerable<AttributeValueReadModel>> GetAllAttributes()
        {
            var attributeValues = await _attributeValueService.GetAllAsync();
            return attributeValues;
        }
    }
}
