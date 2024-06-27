using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TechStore.Application.Interfaces.Services;
using TechStore.Application.Models.PromoCode;


namespace TechStore.API.Controllers
{
    [Route("api/promo-codes")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class PromoCodeController : ControllerBase
    {
        private readonly IPromoCodeService _promoCodeService;
        private readonly ILogger<PromoCodeController> _logger;

        public PromoCodeController(IPromoCodeService promoCodeService, ILogger<PromoCodeController> logger)
        {
            _promoCodeService = promoCodeService;
            _logger = logger;
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PromoCodeCreateModel promoCode)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (promoCode.ExpirationDate <= DateTime.Now)
                return BadRequest("Expiration date must be in the future.");

            var existingPromoCode = await _promoCodeService.GetByCodeAsync(promoCode.Code);

            if (existingPromoCode != null)
                return BadRequest("Promo code already exists.");

            try
            {
                await _promoCodeService.CreateAsync(promoCode);
             
                _logger.LogInformation("Promo code {Code} created with discount {Discount}% and expiration date {ExpirationDate}.", promoCode.Code, promoCode.Discount, promoCode.ExpirationDate);
                return Ok(promoCode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the promo code.");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] PromoCodeUpdateModel promoCode)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id < 1)
            {
                _logger.LogWarning("Invalid promo code ID {Id}.", id);
                return BadRequest("Invalid promo code ID.");
            }


            if (promoCode.ExpirationDate <= DateTime.Now)
                return BadRequest("Expiration date must be in the future.");

            try
            {
                var updatedPromoCode = await _promoCodeService.UpdateAsync(id, promoCode);

                if (updatedPromoCode == null)
                {
                    _logger.LogWarning("Promo code with code {Code} was not found.", promoCode.Code);
                    return NotFound("Promo code not found.");
                }

                _logger.LogInformation("Promo code {Code} updated with discount {Discount}% and expiration date {ExpirationDate}.", promoCode.Code, promoCode.Discount, promoCode.ExpirationDate);
                return Ok(updatedPromoCode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the promo code.");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1)
            {
                _logger.LogWarning("Invalid promo code ID {Id}.", id);
                return BadRequest("Invalid promo code ID.");
            }

            try
            {
                int? deletedPromoCodeId = await _promoCodeService.DeleteAsync(id);

                if (deletedPromoCodeId == null)
                {
                    _logger.LogWarning("Promo code with ID {Id} was not found.", id);
                    return NotFound("Promo code not found.");
                }

                _logger.LogInformation("Promo code with ID {Id} was deleted successfully.", id);
                return Ok(deletedPromoCodeId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the promo code with ID {Id}.", id);
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request.");
            }
        }

        [AllowAnonymous]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation("Received request to fetch promo code with ID {Id}.", id);

            if (id < 1)
            {
                _logger.LogWarning("Invalid promo code ID {Id}.", id);
                return BadRequest("Invalid promo code ID.");
            }

            try
            {
                var promoCode = await _promoCodeService.GetByIdAsync(id);

                if (promoCode == null)
                {
                    _logger.LogWarning("Promo code with ID {Id} not found.", id);
                    return NotFound("Promo code not found.");
                }

                _logger.LogInformation("Promo code with ID {Id} fetched successfully.", id);
                return Ok(promoCode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching promo codes.");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request.");
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Fetching all promo codes.");

            try
            {
                var promoCodes = await _promoCodeService.GetAllAsync();

                _logger.LogInformation("Successfully fetched all promo codes.");
                return Ok(promoCodes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching all promo codes.");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request.");
            }
        }
    }
}
