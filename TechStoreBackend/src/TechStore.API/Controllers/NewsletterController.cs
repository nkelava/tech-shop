using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TechStore.Application.Interfaces.Services;
using TechStore.Application.Models.Newsletter;


namespace TechStore.API.Controllers
{
    [Route("api/[controller]")]
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
        public async Task<IActionResult> Subscribe([FromBody]NewsletterReadModel subscription)
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

        [HttpGet("/api/subscribers")]
        public IActionResult GetSubscribers()
        {
            var subscribers = _newsletterService.GetAllNewsletterSubsribers();

            return Ok(subscribers);
        }
    }
}
