using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TechStore.Application.Interfaces.Services;
using TechStore.Application.Models.Cart;


namespace TechStore.API.Controllers
{
    [Route("api/carts")]
    [Authorize]
    [ApiController]
    public class CartController : ControllerBase
    {
        public readonly ICartService _cartService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public readonly IMapper _mapper;

        public CartController(ICartService cartService, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _cartService = cartService;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }


        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CartCreateModel cart)
        {
            var currentUserEmail = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email);

            if (string.IsNullOrWhiteSpace(currentUserEmail))
                return Unauthorized();

            if (cart.ProductId < 1 || cart.Quantity < 1)
                return BadRequest();

            try
            {
                await _cartService.AddProductAsync(currentUserEmail, cart.ProductId, cart.Quantity);
                return Ok(cart.ProductId);
            } 
            catch
            {
                return NotFound();
            }

        }

        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            var currentUserEmail = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email);

            if (string.IsNullOrWhiteSpace(currentUserEmail))
                return Unauthorized();

            int cartId = await _cartService.ClearCart(currentUserEmail);

            return cartId < 1 ? NotFound() : Ok(cartId);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Remove(int id)
        {
            var currentUserEmail = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email);

            if (string.IsNullOrWhiteSpace(currentUserEmail))
                return Unauthorized();

            if (id < 1)
                return BadRequest();

            try 
            {
                var cart = await _cartService.GetByEmailAsync(currentUserEmail);
                await _cartService.RemoveProductAsync(cart.Id, id);
                
                return Ok(id);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var currentUserEmail = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email);

            if (string.IsNullOrWhiteSpace(currentUserEmail))
                return Unauthorized();

            try
            {
                var cart = await _cartService.GetByEmailAsync(currentUserEmail);

                return (cart is null) ? NotFound() : Ok(cart);
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
