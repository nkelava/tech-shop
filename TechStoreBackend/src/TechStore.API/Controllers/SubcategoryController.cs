using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TechStore.Application.Interfaces.Services;
using TechStore.Application.Models.Subcategory;


namespace TechStore.API.Controllers
{
    [Route("api/subcategories")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class SubcategoryController : ControllerBase
    {
        private readonly ISubcategoryService _subcategoryService;
        private readonly ILogger<SubcategoryController> _logger;
        private static IWebHostEnvironment _webHostEnvironment;


        public SubcategoryController(ISubcategoryService subcategoryService, ILogger<SubcategoryController> logger, IWebHostEnvironment webHostEnvironment)
        {
            _subcategoryService = subcategoryService;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromForm] SubcategoryCreateModel subcategory)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingSubcategory = await _subcategoryService.GetBySlugAsync(subcategory.Slug);

            if (existingSubcategory != null)
                return BadRequest("Subcategory already exists.");

            try
            {
                var subcategoryImage = subcategory?.Image;
                subcategory.Image = null;

                if (subcategoryImage != null && subcategoryImage.Length > 0)
                {
                    string path = _webHostEnvironment.WebRootPath + "\\subcategories\\";

                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);

                    string fileName = subcategory.Slug + ".png";

                    if (System.IO.File.Exists(path + fileName))
                    {
                        System.IO.File.Delete(path + fileName);
                    }

                    using (FileStream fileStream = System.IO.File.Create(path + fileName))
                    {
                        subcategoryImage.CopyTo(fileStream);
                        fileStream.Flush();
                    }

                }

                await _subcategoryService.CreateAsync(subcategory);

                _logger.LogInformation("Sucategory created with name {Name}% and slug {Slug}.", subcategory.Name, subcategory.Slug);
                return Ok(subcategory);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the subcategory.");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] SubcategoryUpdateModel subcategory)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id < 1)
            {
                _logger.LogWarning("Invalid sucategory ID {Id}.", id);
                return BadRequest("Invalid sucategory ID.");
            }

            try
            {
                var updatedSubcategory = await _subcategoryService.UpdateAsync(id, subcategory);

                string fileName = updatedSubcategory?.Slug + ".png";
                var path = Path.Combine(_webHostEnvironment.WebRootPath, "subcategories", fileName);

                if (System.IO.File.Exists(path))
                {
                    updatedSubcategory.ImageByte = System.IO.File.ReadAllBytes(path);
                }


                if (updatedSubcategory == null)
                {
                    _logger.LogWarning("Subcategory with ID {Id} was not found.", id);
                    return NotFound("Subcategory not found.");
                }

                _logger.LogInformation("Subcategory with ID {Id} updated successfully.", updatedSubcategory.Id);
                return Ok(updatedSubcategory);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the subcategory.");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1)
            {
                _logger.LogWarning("Invalid sucategory ID {Id}.", id);
                return BadRequest("Invalid sucategory ID.");
            }

            try
            {
                int? deletedSubcategoryId = await _subcategoryService.DeleteAsync(id);

                if (deletedSubcategoryId == null)
                {
                    _logger.LogWarning("Subcategory with ID {Id} was not found.", id);
                    return NotFound("Subcategory not found.");
                }

                _logger.LogInformation("Subcategory with ID {Id} was deleted successfully.", id);
                return Ok(deletedSubcategoryId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the subcategory with ID {Id}.", id);
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request.");
            }
        }

        [AllowAnonymous]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation("Received request to fetch subcategory with ID {Id}.", id);

            if (id < 1)
            {
                _logger.LogWarning("Invalid subcategory ID {Id}.", id);
                return BadRequest("Invalid subcategory ID.");
            }

            try
            {
                var subcategory = await _subcategoryService.GetByIdAsync(id);
                string fileName = subcategory?.Slug + ".png";
                var path = Path.Combine(_webHostEnvironment.WebRootPath, "subcategories", fileName);

                if (System.IO.File.Exists(path))
                {
                    subcategory.ImageByte = System.IO.File.ReadAllBytes(path);
                }

                if (subcategory == null)
                {
                    _logger.LogWarning("Subcategory with ID {Id} not found.", id);
                    return NotFound("Subcategory not found.");
                }

                _logger.LogInformation("Subcategory with ID {Id} fetched successfully.", id);
                return Ok(subcategory);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching subcategories.");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request.");
            }
        }

        [AllowAnonymous]
        [HttpGet("{slug}")]
        public async Task<IActionResult> GetBySlug(string slug)
        {
            if (string.IsNullOrWhiteSpace(slug))
            {
                _logger.LogWarning("Invalid subcategory slug {Slug}.", slug);
                return BadRequest("Invalid subcategory slug.");
            }

            try
            {
                var subcategory = await _subcategoryService.GetBySlugAsync(slug);
                string fileName = subcategory?.Slug + ".png";
                var path = Path.Combine(_webHostEnvironment.WebRootPath, "subcategories", fileName);

                if (System.IO.File.Exists(path))
                {
                    subcategory.ImageByte = System.IO.File.ReadAllBytes(path);
                }


                if (subcategory == null)
                {
                    _logger.LogWarning("Subcategory with slug {Slug} not found.", slug);
                    return NotFound("Subcategory not found.");
                }

                _logger.LogInformation("Subcategory with slug {Slug} fetched successfully.", slug);
                return Ok(subcategory);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching subcategory.");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request.");
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Fetching all subcategories.");
            try
            {
                var subcategories = await _subcategoryService.GetAllAsync();
                string fileName = "";
                string path = "";

                foreach (var subcategory in subcategories)
                {
                    fileName = subcategory?.Slug + ".png";
                    path = Path.Combine(_webHostEnvironment.WebRootPath, "subcategories", fileName);

                    if (System.IO.File.Exists(path))
                    {
                        subcategory.ImageByte = System.IO.File.ReadAllBytes(path);
                    }
                }

                _logger.LogInformation("Successfully fetched all subcategories.");
                return Ok(subcategories);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching subcategories.");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request.");
            }
        }
    }
}
