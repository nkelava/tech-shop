using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(string username, int productId)
        {
            if (username is null || productId < 1)
                return BadRequest();

            await _wishlistService.AddProductAsync(username, productId);

            return Ok(productId);
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> Delete(int wishlistId, int productId)
        {
            if (wishlistId < 1 || productId < 1)
                return BadRequest();

            await _wishlistService.RemoveProductAsync(wishlistId, productId);

            return Ok(productId);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetWishlistByUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return BadRequest();

            var wishlist = await _wishlistService.GetByUsernameAsync(username);

            return (wishlist is null) ? NotFound() : Ok(wishlist);
        }
    }
}
