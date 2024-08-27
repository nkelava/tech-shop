using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using TechStore.Application.Interfaces.Services;
using TechStore.Application.Models.Cart;
using TechStore.Domain.Entities.User;


namespace TechStore.API.Controllers
{
    [Route("api/carts")]
    [Authorize]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<CartController> _logger;


        public CartController(ICartService cartService, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor, ILogger<CartController> logger)
        {
            _cartService = cartService;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }


        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CartCreateModel cart)
        {
            var currentUserEmail = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email);

            if (string.IsNullOrWhiteSpace(currentUserEmail))
            {
                _logger.LogWarning("Unauthorized access attempt.");
                return Unauthorized("User is not authorized.");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid cart model received: {ModelStateErrors}", ModelState);
                return BadRequest("Invalid cart data. Please check the details and try again.");
            }

            try
            {
                var currentUser = await _userManager.FindByEmailAsync(currentUserEmail);
                var response = await _cartService.AddProductAsync(currentUser, cart);

                if (response == null)
                {
                    _logger.LogWarning("Product with ID {id} was not found.", cart.ProductId);
                    return NotFound("Product not found.");
                }
               
                _logger.LogInformation("User with email {Email} updated cart: Product ID: {ID}, Quantity: {Quantity}.", currentUserEmail, cart.ProductId, cart.Quantity);
                return Ok(cart);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the cart for user {Email}.", currentUserEmail);
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            var currentUserEmail = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email);

            if (string.IsNullOrWhiteSpace(currentUserEmail))
            {
                _logger.LogWarning("Unauthorized access attempt with no email found in claims.");
                return Unauthorized();
            }


            try
            {
                var currentUser = await _userManager.FindByEmailAsync(currentUserEmail);
                var cart = await _cartService.ClearCart(currentUser);

                if (cart == null)
                {
                    _logger.LogInformation("No cart found to clear for user with email {Email}.", currentUserEmail);
                    return NotFound();
                }

                _logger.LogInformation("Cart successfully cleared for user with email {Email}.", currentUserEmail);
                return Ok(cart);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while clearing cart for user.");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request.");
            }
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Remove(int id)
        {
            var currentUserEmail = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email);

            if (string.IsNullOrWhiteSpace(currentUserEmail))
            {
                _logger.LogWarning("Unauthorized access attempt.");
                return Unauthorized("User is not authorized.");
            }

            if (id < 1)
            {
                _logger.LogWarning("Invalid product ID {Id}.", id);
                return BadRequest("Invalid product ID.");
            }


            try 
            {
                var currentUser = await _userManager.FindByEmailAsync(currentUserEmail);
                var cart = await _cartService.GetAsync(currentUser);

                if (cart == null)
                {
                    _logger.LogWarning("Cart for user {Email} was not found.", currentUserEmail);
                    return NotFound("Cart not found.");
                }

                var response = await _cartService.RemoveProductAsync(cart.Id, id);

                if (response == null)
                {
                    _logger.LogWarning("Product with ID {id} was not found.", id);
                    return NotFound("Product not found.");
                }

                _logger.LogInformation("User with email {Email} removed product with ID {Id} from cart.", currentUserEmail, id);
                return Ok(cart);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while removing the product from the cart.");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var currentUserEmail = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email);

            if (string.IsNullOrWhiteSpace(currentUserEmail))
            {
                _logger.LogWarning("Unauthorized access attempt.");
                return Unauthorized("User is not authorized.");
            }

            try
            {
                var currentUser = await _userManager.FindByEmailAsync(currentUserEmail);
                var cart = await _cartService.GetAsync(currentUser);

                if (cart == null)
                {
                    _logger.LogWarning("Cart for user {Email} was not found.", currentUserEmail);
                    return NotFound("Cart not found.");
                }

                _logger.LogInformation("Cart with email {Email} fetched successfully.", currentUserEmail);
                return Ok(cart);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching cart.");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request.");
            }
        }
    }
}
