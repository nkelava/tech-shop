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
        public readonly ILogger<ReviewController> _logger;


        public ReviewController(IReviewService reviewService, IHttpContextAccessor httpContextAccessor, ILogger<ReviewController> logger)
        {
            _reviewService = reviewService;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ReviewCreateModel review)
        {
            var currentUserEmail =  _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (currentUserEmail == null)
                return Unauthorized("Unauthorized access: User is not authenticated.");

            try {
                await _reviewService.CreateAsync(currentUserEmail, review);
                
                _logger.LogInformation("Review from email {Email} wa created with comment {Comment} and rate {Rate}.", currentUserEmail, review.Comment, review.Rate);
                return Ok(review);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the review.");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1)
                return BadRequest("Invalid review ID.");

            try
            {
                int? deletedReviewId = await _reviewService.DeleteAsync(id);

                if (deletedReviewId == null)
                {
                    _logger.LogWarning("Review with ID {Id} was not found.", id);
                    return NotFound("Review not found.");
                }

                _logger.LogInformation("Review with ID {Id} was deleted successfully.", id);
                return Ok(deletedReviewId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the review with ID {Id}.", id);
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request.");
            }
        }

        [AllowAnonymous]
        [HttpGet("{productId:int}")]
        public async Task<IActionResult> GetByProductId(int productId)
        {
            _logger.LogInformation("Received request to reviews by product ID {Id}.", productId);

            if (productId < 1)
            {
                _logger.LogWarning("Invalid product ID {Id}.", productId);
                return BadRequest("Invalid product ID.");
            }

            try
            {
                var reviews = await _reviewService.GetByProductIdAsync(productId);

                if (reviews == null)
                {
                    _logger.LogWarning("Reviews with product ID {Id} not found.", productId);
                    return NotFound("Reviews not found.");
                }

                _logger.LogInformation("Reviews with product ID {Id} fetched successfully.", productId);
                return Ok(reviews);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching reviews.");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request.");
            }
        }

        [AllowAnonymous]
        [HttpGet("{email}")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                _logger.LogWarning("Invalid email {Email}.", email);
                return BadRequest("Invalid email.");
            }

            try
            {
                var reviews = await _reviewService.GetByEmailAsync(email);

                if (reviews == null)
                {
                    _logger.LogWarning("Reviews with email {Email} not found.", email);
                    return NotFound("Reviews not found.");
                }

                _logger.LogInformation("Reviews with email {Email} fetched successfully.", email);
                return Ok(reviews);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching reviews.");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllReviews()
        {
            _logger.LogInformation("Received request to fetch all reviews.");

            try
            {
                var reviews = await _reviewService.GetAllAsync();
                _logger.LogInformation("Successfully fetched all reviews.");
                return Ok(reviews);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching reviews.");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request.");
            }
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
            _logger.LogInformation("Received request to fetch all reported reviews.");

            try
            {
                var reportedReviews = await _reviewService.GetAllReportedReviewsAsync();

                _logger.LogInformation("Successfully fetched all reported reviews.");
                return Ok(reportedReviews);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching reported reviews.");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request.");
            }
        }
    }
}
