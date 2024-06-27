using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechStore.Application.Interfaces.Services;
using TechStore.Application.Models.Newsletter;


namespace TechStore.API.Controllers
{
    [Route("api/newsletters")]
    [ApiController]
    public class NewsletterController : ControllerBase
    {
        private readonly INewsletterService _newsletterService;
        private readonly ILogger<NewsletterController> _logger;

        public NewsletterController(INewsletterService newsletterService, ILogger<NewsletterController> logger)
        {
            _newsletterService = newsletterService;
            _logger = logger;

        }


        [HttpPost]
        public async Task<IActionResult> Subscribe([FromBody]NewsletterCreateModel subscription)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid subscription model received: {ModelStateErrors}", ModelState);
                return BadRequest(ModelState);
            }

            try
            {
                var newsletter = await _newsletterService.Subscribe(subscription);

                if (newsletter == null)
                {
                    _logger.LogWarning("Attempt to subscribe an already subscribed email: {Email}", subscription.Email);
                    return Conflict("Email is already subscribed.");
                }

                _logger.LogInformation("Successfully subscribed email: {Email}", subscription.Email);
                return Ok(subscription);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred during subscription for email: {Email}", subscription.Email);
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Unsubscribe([FromBody] NewsletterCreateModel subscription)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid subscription model received: {ModelStateErrors}", ModelState);
                return BadRequest(ModelState);
            }

            try
            {
                var unsubscribedEmail= await _newsletterService.Unsubscribe(subscription.Email);

                if (unsubscribedEmail == null)
                {
                    _logger.LogWarning("Subscribtion with email {Email} not found.", subscription.Email);
                    return Conflict("Email is not found.");
                }

                _logger.LogInformation("Successfully unsubscribed email: {Email}", subscription.Email);
                return Ok(subscription.Email);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred during unsubscription for email: {Email}", subscription.Email);
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetSubscribers()
        {
            _logger.LogInformation("Fetching all subscribers.");

            try
            {
                var subscribers = await _newsletterService.GetAllSubscribersAsync();

                _logger.LogInformation("Successfully fetched all subscribers.");
                return Ok(subscribers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching all newsletter subscribers.");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request.");
            }
        }
    }
}
