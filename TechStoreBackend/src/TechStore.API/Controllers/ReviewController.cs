using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using TechStore.Application.Interfaces.Services;
using TechStore.Application.Models.Review;


namespace TechStore.API.Controllers
{
    [Route("api/reviews")]
    [Authorize]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        public readonly IReviewService _reviewService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public readonly IMapper _mapper;
        public readonly ILogger<ReviewController> _logger;


        public ReviewController(IReviewService reviewService, IHttpContextAccessor httpContextAccessor, IMapper mapper, ILogger<ReviewController> logger)
        {
            _reviewService = reviewService;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Create(ReviewCreateModel review)
        {
            var currentUserEmail =  _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email);

            if (review is null)
                return BadRequest();

            if (currentUserEmail is null)
                return Unauthorized();

            try {
                await _reviewService.CreateAsync(currentUserEmail, review);
                return Ok();
            } catch {
                return BadRequest();
            }
        }

        [HttpDelete("{reviewId:int}")]
        public async Task<IActionResult> Delete(int reviewId)
        {
            if (reviewId < 1)
                return BadRequest();

            try {
                await _reviewService.DeleteAsync(reviewId);

                return Ok(reviewId);
            } catch {
                return BadRequest();
            }
        }

        [AllowAnonymous]
        [HttpGet("{productId:int}")]
        public async Task<IActionResult> GetReviewsByProductId(int productId)
        {
            if (productId < 1)
                return BadRequest();

            try
            {
                var reviews = await _reviewService.GetReviewsByProductIdAsync(productId);

                return (reviews is null) ? NotFound() : Ok(reviews);
            } catch {
                return BadRequest();
            }
        }

        [AllowAnonymous]
        [HttpGet("{email}")]
        public async Task<IActionResult> GetReviewsByEmail(string email)
        {
            if (email is null)
                return BadRequest();

            var reviews = await _reviewService.GetReviewsByEmailAsync(email);

            return (reviews is null) ? NotFound() : Ok(reviews);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReviews()
        {
            var reviews = await _reviewService.GetAllReviewsAsync();
            return Ok(reviews);
        }

        [HttpPost("report/{id:int}")]
        public async Task<IActionResult> ReportReview(int id, [FromBody] ReviewReportModel reportedReview)
        {
            _logger.LogInformation("Received request to report review with ID {Id}.", id);

            if (id < 1)
            {
                _logger.LogWarning("Invalid review ID {Id}.", id);
                return BadRequest("Invalid review ID.");
            }

            try
            {
                var review = await _reviewService.ReportReviewAsync(id, reportedReview.IsReported);

                if (review == null)
                {
                    _logger.LogWarning("Review with ID {Id} not found.", id);
                    return NotFound("Review not found.");
                }

                _logger.LogInformation("Review with ID {Id} fetched successfully.", id);
                return Ok(review);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching promo codes.");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request.");
            }
        }


        [HttpGet("reported")]
        public async Task<IActionResult> GetAllReportedReviews()
        {
            var reviews = await _reviewService.GetAllReportedReviewsAsync();
            return Ok(reviews);
        }
    }
}
