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
        public async Task<IActionResult> Add(ReviewCreateModel review)
        {
            if (review == null)
                return BadRequest();

            await _reviewService.AddReview(review);

            return Ok(review);
        }
        
        [HttpDelete]
        public async Task<IActionResult> Delete(int reviewId)
        {
            if (reviewId < 1)
                return BadRequest();

            await _reviewService.DeleteReview(reviewId);

            return Ok(reviewId);
        }

        [HttpGet("{productId:int}")]
        public async Task<IActionResult> GetReviewsByProductId(int productId)
        {
            if (productId < 1)
                return BadRequest();

            var reviews = _reviewService.GetReviewsByProductId(productId);

            if (reviews == null)
                return NotFound();

            return Ok(reviews);
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> GetReviewsByEmail(string email)
        {
            if (email == null || email.Length == 0)
                return BadRequest();

            var reviews = _reviewService.GetReviewsByEmail(email);

            if (reviews == null)
                return NotFound();

            return Ok(reviews);
        }
    }
}
