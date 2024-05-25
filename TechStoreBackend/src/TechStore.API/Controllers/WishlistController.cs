using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TechStore.Application.Interfaces.Services;
using TechStore.Application.Models.Wishlist;


namespace TechStore.API.Controllers
{
    [Route("api/wishlists")]
    [Authorize]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        public readonly IWishlistService _wishlistService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public readonly IMapper _mapper;

        public WishlistController(IWishlistService wishlistService, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _wishlistService = wishlistService;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] WishlistAddProductModel wishlist)
        {
            var currentUserEmail = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email);

            if (string.IsNullOrWhiteSpace(currentUserEmail))
                return Unauthorized();

            if (wishlist.ProductId < 1)
                return BadRequest();

            try
            {
                await _wishlistService.AddProductAsync(currentUserEmail, wishlist.ProductId);
                return Ok(wishlist.ProductId);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var currentUserEmail = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email);

            if (string.IsNullOrWhiteSpace(currentUserEmail))
                return Unauthorized();

            if (id < 1)
                return BadRequest();

            var wishlist = await _wishlistService.GetByEmailAsync(currentUserEmail);
            int productId = await _wishlistService.RemoveProductAsync(wishlist.Id, id);
             
            return productId > 0 ? Ok(id) : NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var currentUserEmail = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email);

            if (string.IsNullOrWhiteSpace(currentUserEmail) )
                return Unauthorized();

            var wishlist = await _wishlistService.GetByEmailAsync(currentUserEmail);

            return (wishlist is null) ? NotFound() : Ok(wishlist);
        }
    }
}
