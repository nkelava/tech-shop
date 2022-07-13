using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TechStore.Application.Interfaces.Services;


namespace TechStore.API.Controllers
{
    [Route("api/wishlist")]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        public readonly IWishlistService _wishlistService;
        public readonly IMapper _mapper;

        public WishlistController(IWishlistService wishlistService, IMapper mapper)
        {
            _wishlistService = wishlistService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Add(string username, int productId)
        {
            if (username == null || productId < 1)
                return BadRequest();

            await _wishlistService.AddProductAsync(username, productId);

            return Ok(productId);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int wishlistId, int productId)
        {
            if (wishlistId < 1 || productId < 1)
                return BadRequest();

            await _wishlistService.RemoveProductAsync(wishlistId, productId);

            return Ok(productId);
        }

        [HttpGet]
        public async Task<IActionResult> GetWishlistByUsername(string username)
        {
            if (username == null)
                return BadRequest();

            if (username.Length == 0)
                return BadRequest();

            var wishlist = await _wishlistService.GetByUsernameAsync(username);

            if (wishlist == null)
                return NotFound();

            return Ok(wishlist);
        }
    }
}
