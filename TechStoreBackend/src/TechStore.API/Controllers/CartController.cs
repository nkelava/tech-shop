using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechStore.Application.Interfaces.Services;
using TechStore.Application.Models.Cart;

namespace TechStore.API.Controllers
{
    [Route("api/[controller]")]
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

        [HttpGet("/cart/:{username}")]
        public async Task<IActionResult> GetByUsername(string username)
        {
            if (username == null)
                return BadRequest();

            if (username.Length == 0)
                return BadRequest();

            var cart = await _cartService.GetByUsername(username);

            if (cart == null)
                return NotFound();

            return Ok(cart);
        }


        [HttpPost]
        public async Task<IActionResult> Add(string username, int productId)
        {
            if (username == null || productId < 1)
                return BadRequest();

            await _cartService.AddProduct(username, productId);

            return Ok();
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


        [HttpDelete]
        public async Task<IActionResult> Delete(int cartId, int productId)
        {
            if (cartId < 1 || productId < 1)
                return BadRequest();

            await _cartService.RemoveProduct(cartId, productId);

            return Ok();
        }
    }
}
