using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TechStore.Application.Interfaces.Services;
using TechStore.Application.Models.Attribute;


namespace TechStore.API.Controllers
{
    [Route("api/attributes")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class AttributeController : ControllerBase
    {
        private readonly IAttributeService _attributeService;
        private readonly ILogger<AttributeController> _logger;

        public AttributeController(IAttributeService attributeService, ILogger<AttributeController> logger)
        {
            _attributeService = attributeService;
            _logger = logger;
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AttributeCreateModel attribute)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var existingAttribute = await _attributeService.GetByNameAsync(attribute.Name);

            if (existingAttribute != null)
                return BadRequest("Attribute already exists.");

            try
            {
                await _attributeService.CreateAsync(attribute);

                _logger.LogInformation("Attribute with name {Name} created successfully.", attribute.Name);
                return Ok(attribute);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the attribute.");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] AttributeUpdateModel attribute)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var updatedAttribute = await _attributeService.UpdateAsync(id, attribute);
                
                if (updatedAttribute == null)
                {
                    _logger.LogWarning("Attribute with ID {Id} was not found.", id);
                    return NotFound("Attribute not found.");
                }

                _logger.LogInformation("Attribute with ID {Id} and name {Name} updated with new name {NewName}.", id, attribute.Name, updatedAttribute.Name);
                return Ok(updatedAttribute);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the attribute.");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1)
            {
                _logger.LogWarning("Invalid attribute ID {Id}.", id);
                return BadRequest("Invalid attribute ID.");
            }

            try
            {
                int? deletedAttributeId = await _attributeService.DeleteAsync(id);

                if (deletedAttributeId == null)
                {
                    _logger.LogWarning("Attribute with ID {Id} was not found.", id);
                    return NotFound("Attribute not found.");
                }

                _logger.LogInformation("Attribute with ID {Id} was deleted successfully.", id);
                return Ok(deletedAttributeId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the attribute with ID {Id}.", id);
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request.");
            }

        }

        [AllowAnonymous]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation("Received request to fetch attribute with ID {Id}.", id);

            if (id < 1)
            {
                _logger.LogWarning("Invalid attribute ID {Id}.", id);
                return BadRequest("Invalid attribute ID.");
            }

            try
            {
                var attribute = await _attributeService.GetByIdAsync(id);

                if (attribute == null)
                {
                    _logger.LogWarning("Attribute with ID {Id} not found.", id);
                    return NotFound("Attribute not found.");
                }

                _logger.LogInformation("Attribute with ID {Id} fetched successfully.", id);
                return Ok(attribute);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching attribute with ID {Id}.", id);
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request.");
            }
        }

        [AllowAnonymous]
        [HttpGet("{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            _logger.LogInformation("Received request to fetch attribute with name {Name}.", name);

            if (string.IsNullOrWhiteSpace(name))
            {
                _logger.LogWarning("Invalid attribute name {Name}.", name);
                return BadRequest("Invalid attribute name.");
            }

            try
            {
                var attribute = await _attributeService.GetByNameAsync(name);

                if (attribute == null)
                {
                    _logger.LogWarning("Attribute with name {Name} not found.", name);
                    return NotFound("Attribute not found.");
                }

                _logger.LogInformation("Attribute with name {Name} fetched successfully.", name);
                return Ok(attribute);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching attribute with name {Name}.", name);
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request.");
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Fetching all attributes.");

            try
            {
                var attributes = await _attributeService.GetAllAsync();
                
                _logger.LogInformation("Successfully fetched all attributes.");
                return Ok(attributes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching all attributes.");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request.");
            }
        }
    }
}
