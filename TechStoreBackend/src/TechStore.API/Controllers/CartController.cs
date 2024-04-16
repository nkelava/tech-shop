using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechStore.Application.Interfaces.Services;


namespace TechStore.API.Controllers
{
    [Route("api/carts")]
    [ApiController]
    public class CartController : ControllerBase
    {
        public readonly ICartService _cartService;
        public readonly IMapper _mapper;

        public CartController(ICartService cartService, IMapper mapper)
        {
            _cartService = cartService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Add(string username, int quantity, int productId)
        {
            if (username is null || productId < 1)
                return BadRequest();

            await _cartService.AddProductAsync(username, quantity, productId);

            return Ok(productId);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int cartId, int productId)
        {
            if (cartId < 1 || productId < 1)
                return BadRequest();

            await _cartService.RemoveProductAsync(cartId, productId);

            return Ok(productId);
        }

        //[HttpPut]
        //public async Task<IActionResult> Update([FromBody] CartUpdateModel cart)
        //{
        //    if (cart == null)
        //        return BadRequest();
        //    // TODO: implement Update
        //    await _cartService.Update(cart);

        //    return Ok();
        //}

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetByEmail(string email)
        {
            if (email is null)
                return BadRequest();

            var cart = await _cartService.GetByEmail(email);

            return (cart is null) ? NotFound() : Ok(cart);
        }
    }
}
