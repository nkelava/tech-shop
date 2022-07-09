using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TechStore.Application.Interfaces.Services;
using TechStore.Application.Models.Wishlist;

namespace TechStore.API.Controllers
{
    [Route("api/[controller]")]
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

        [HttpGet("/{username}")]
        public async Task<IActionResult> GetWishlistByUsername(string username)
        {
            if (username == null)
                return BadRequest();

            if(username.Length == 0)
                return BadRequest();

            var wishlist = await _wishlistService.GetByUsernameAsync(username);

            if (wishlist == null)
                return NotFound();

            return Ok(wishlist);
        }


        [HttpPost]
        public async Task<IActionResult> Add([FromBody]WishlistReadModel wishlist)
        {
            if (wishlist == null)
                return BadRequest();

            await _wishlistService.AddItem(wishlist.Username, wishlist.WishlistProducts.First().Id);

            return Ok();
        }


        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody]WishlistRemoveProductModel wishlistRemoveProductModel)
        {
            if (wishlistRemoveProductModel.WishlistId < 1 || wishlistRemoveProductModel.ProductId < 1)
                return BadRequest();

            await _wishlistService.RemoveItem(wishlistRemoveProductModel.WishlistId, wishlistRemoveProductModel.ProductId);

            return Ok();
        }
    }
}
