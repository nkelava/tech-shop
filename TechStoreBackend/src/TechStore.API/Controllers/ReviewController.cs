using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TechStore.Application.Interfaces.Services;
using TechStore.Application.Models.Review;
using TechStore.Domain.Entities.ProductAggregate;


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

        public ReviewController(IReviewService reviewService, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _reviewService = reviewService;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
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

        [HttpPut]
        public async Task<IActionResult> ReportReview(ReviewReportModel reportModel)
        {
            var currentUserEmail = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email);

            if (currentUserEmail is null)
                return Unauthorized();

            if (reportModel.Id < 1)
                return BadRequest();

            var updatedReviewId = await _reviewService.ReportReviewAsync(reportModel.Id, reportModel.IsReported);

            return updatedReviewId < 1 ? NotFound() : Ok(updatedReviewId);
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


        [HttpGet("/reported")]
        public async Task<IActionResult> GetAllReportedReviews()
        {
            var reviews = await _reviewService.GetAllReportedReviewsAsync();
            return Ok(reviews);
        }
    }
}
