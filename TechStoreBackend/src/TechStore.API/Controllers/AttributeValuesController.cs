using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TechStore.Application.Interfaces.Services;
using TechStore.Application.Models.AttributeValue;


namespace TechStore.API.Controllers
{
    [Route("api/attribute-values")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class AttributeValuesController : ControllerBase
    {
        private readonly IAttributeValueService _attributeValueService;
        private readonly ILogger<AttributeValuesController> _logger;

        public AttributeValuesController(IAttributeValueService attributeValueService, ILogger<AttributeValuesController> logger)
        {
            _attributeValueService = attributeValueService;
            _logger = logger;
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AttributeValueCreateModel attributeValue)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _attributeValueService.CreateAsync(attributeValue);

                _logger.LogInformation("Attribute value {Value} created successfully.", attributeValue.Value);
                return Ok(attributeValue);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the attribute value {Value}.", attributeValue.Value);
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] AttributeValueUpdateModel attributeValue)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id < 1)
            {
                _logger.LogWarning("Invalid attribute value ID {Id}.", id);
                return BadRequest("Invalid attribute value ID.");
            }

            try
            {
                var updatedAttributeValue = await _attributeValueService.UpdateAsync(id, attributeValue);

                if (updatedAttributeValue == null)
                {
                    _logger.LogWarning("Attribute value with ID {Id} was not found.", id);
                    return NotFound("Attribute value not found.");
                }

                _logger.LogInformation("Attribute value {Value} with ID {Id} updated with value {NewValue}.", id, attributeValue.Value, updatedAttributeValue.Value);
                return Ok(updatedAttributeValue);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the attribute value {Id}.", id);
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request.");
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1)
            {
                _logger.LogWarning("Invalid attribute value ID {Id}.", id);
                return BadRequest("Invalid attribute value ID.");
            }

            try
            {
                int? deletedAttributeValueId = await _attributeValueService.DeleteAsync(id);

                if (deletedAttributeValueId == null)
                {
                    _logger.LogWarning("Attribute value with ID {Id} was not found.", id);
                    return NotFound("Attribute value not found.");
                }

                _logger.LogInformation("Attribute value with ID {Id} was deleted successfully.", id);
                return Ok(deletedAttributeValueId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the attribute value with ID {Id}.", id);
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request.");
            }

        }

        [AllowAnonymous]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation("Received request to fetch attribute value with ID {Id}.", id);

            if (id < 1)
            {
                _logger.LogWarning("Invalid attribute value ID {Id}.", id);
                return BadRequest("Invalid attribute value ID.");
            }

            try
            {
                var attributeValue = await _attributeValueService.GetByIdAsync(id);

                if (attributeValue == null)
                {
                    _logger.LogWarning("Attribute value with ID {Id} not found.", id);
                    return NotFound("Attribute value not found.");
                }

                _logger.LogInformation("Attribute value with ID {Id} fetched successfully.", id);
                return Ok(attributeValue);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching attribute value.");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request.");
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Fetching all attribute values.");

            try
            {
                var attributeValues = await _attributeValueService.GetAllAsync();

                _logger.LogInformation("Successfully fetched all attribute values.");
                return Ok(attributeValues);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching all attribute values.");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request.");
            }
        }
    }
}
