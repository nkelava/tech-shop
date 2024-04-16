using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TechStore.Application.Interfaces.Services;
using TechStore.Application.Models.Review;


namespace TechStore.API.Controllers
{
    [Route("api/reviews")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        public readonly IReviewService _reviewService;
        public readonly IMapper _mapper;

        public ReviewController(IReviewService reviewService, IMapper mapper)
        {
            _reviewService = reviewService;
            _mapper = mapper;
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(ReviewCreateModel review)
        {
            if (review is null)
                return BadRequest();

            await _reviewService.CreateAsync(review);

            return Ok(review);
        }
        
        [HttpDelete]
        public async Task<IActionResult> Delete(int reviewId)
        {
            if (reviewId < 1)
                return BadRequest();

            await _reviewService.DeleteAsync(reviewId);

            return Ok(reviewId);
        }

        [HttpGet("{productId:int}")]
        public async Task<IActionResult> GetReviewsByProductId(int productId)
        {
            if (productId < 1)
                return BadRequest();

            var reviews = await _reviewService.GetReviewsByProductIdAsync(productId);

            return (reviews is null) ? NotFound() : Ok(reviews);
        }

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
    }
}
