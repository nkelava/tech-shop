using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using TechStore.Application.Interfaces.Services;
using TechStore.Domain.Entities.User;


namespace TechStore.API.Controllers
{
    [Route("api/wishlists")]
    [Authorize]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        private readonly IWishlistService _wishlistService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<WishlistController> _logger;

        public WishlistController(IWishlistService wishlistService, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor, ILogger<WishlistController> logger)
        {
            _wishlistService = wishlistService;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }


        [HttpPost("{productId:int}")]
        public async Task<IActionResult> Add(int productId)
        {
            var currentUserEmail = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email);

            if (string.IsNullOrWhiteSpace(currentUserEmail))
            {
                _logger.LogWarning("Unauthorized access attempt.");
                return Unauthorized("User is not authorized.");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid wishlist model received: {ModelStateErrors}", ModelState);
                return BadRequest("Invalid wishlist data. Please check the details and try again.");
            }

            try
            {
                var currentUser = await _userManager.FindByEmailAsync(currentUserEmail);
                var wishlist = await _wishlistService.AddProductAsync(currentUser, productId);

                if (wishlist == null)
                {
                    _logger.LogWarning("Product with ID {id} was not found.", productId);
                    return NotFound("Product not found.");
                }

                _logger.LogInformation("User with email {Email} added product with {Id} to wishlist.", currentUserEmail, productId);
                return Ok(wishlist);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding product with {Id} to the wishlist for user {Email}.", productId, currentUserEmail);
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpDelete("{productId:int}")]
        public async Task<IActionResult> Remove(int productId)
        {
            var currentUserEmail = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email);

            if (string.IsNullOrWhiteSpace(currentUserEmail))
            {
                _logger.LogWarning("Unauthorized access attempt.");
                return Unauthorized("User is not authorized.");
            }

            if (productId < 1)
            {
                _logger.LogWarning("Invalid product ID {Id}.", productId);
                return BadRequest("Invalid product ID.");
            }

            try
            {
                var currentUser = await _userManager.FindByEmailAsync(currentUserEmail);
                var wishlist = await _wishlistService.GetAsync(currentUser);

                if (wishlist == null)
                {
                    _logger.LogWarning("Wishlist for user {Email} was not found.", currentUserEmail);
                    return NotFound("Wishlist not found.");
                }

                var response = await _wishlistService.RemoveProductAsync(wishlist.Id, productId);

                if (response == null)
                {
                    _logger.LogWarning("Product with ID {id} was not found.", productId);
                    return NotFound("Product not found.");
                }

                _logger.LogInformation("User with email {Email} removed product with ID {Id} from wishlist.", currentUserEmail, productId);
                return Ok(wishlist);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while removing the product from the wishlist.");
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
                var wishlist = await _wishlistService.GetAsync(currentUser);

                if (wishlist == null)
                {
                    _logger.LogWarning("Wishlist for user {Email} was not found.", currentUserEmail);
                    return NotFound("Wishlist not found.");
                }

                _logger.LogInformation("Wishlist with email {Email} fetched successfully.", currentUserEmail);
                return Ok(wishlist);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching wishlist.");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request.");
            }
        }
    }
}
