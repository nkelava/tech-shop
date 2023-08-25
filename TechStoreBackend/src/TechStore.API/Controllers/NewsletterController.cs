using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TechStore.Application.Interfaces.Services;
using TechStore.Application.Models.Newsletter;


namespace TechStore.API.Controllers
{
    [Route("api/newsletters")]
    [ApiController]
    public class NewsletterController : ControllerBase
    {
        public readonly INewsletterService _newsletterService;
        public readonly IMapper _mapper;

        public NewsletterController(INewsletterService newsletterService, IMapper mapper)
        {
            _newsletterService = newsletterService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Subscribe([FromBody]NewsletterCreateModel subscription)
        {
            var email = subscription.Email;

            if (email == null || email.Length < 1)
                return BadRequest();

            await _newsletterService.Subscribe(email);

            return Ok(email);
        }

        [HttpDelete]
        public async Task<IActionResult> Unsubscribe(string email)
        {
            if (email == null || email.Length < 1)
                return BadRequest();

            await _newsletterService.Unsubscribe(email);

            return Ok(email);
        }

        [HttpGet]
        public async Task<IActionResult> GetSubscribers()
        {
            var subscribers = await _newsletterService.GetAllNewsletterSubsribersAsync();

            return Ok(subscribers);
        }
    }
}
