using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using TechStore.Application.Interfaces.Services;
using TechStore.Application.Models.Order;
using TechStore.Domain.Enums.Order;


namespace TechStore.API.Controllers
{
    [Route("api/orders")]
    [Authorize]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<OrderController> _logger;

        public OrderController(IOrderService orderService, IHttpContextAccessor httpContextAccessor, ILogger<OrderController> logger)
        {
            _orderService = orderService;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }


        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderCreateModel order)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid order model state.");
                return BadRequest(ModelState);
            }

            try
            {
                await _orderService.CreateAsync(order);
                return Ok(order);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the order.");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1)
            {
                _logger.LogWarning("Delete called with invalid ID: {Id}", id);
                return BadRequest("Invalid order ID.");
            }

            try
            {
                bool isDeleted = await _orderService.DeleteAsync(id);

                if (!isDeleted)
                {
                    _logger.LogWarning("Order with ID: {Id} not found.", id);
                    return NotFound($"Order with ID {id} not found.");
                }

                _logger.LogInformation("Order with ID: {Id} successfully deleted.", id);
                return Ok($"Order with ID {id} successfully deleted.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting order with ID: {Id}", id);
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpPut("status")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateOrderStatus(OrderUpdateStatusModel updateOrderModel)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid updateOrderModel received.");
                return BadRequest(ModelState);
            }

            if (!CheckIfOrderStatusExists(updateOrderModel.OrderStatusValue))
            {
                _logger.LogWarning("Order status value {OrderStatusValue} not found.", updateOrderModel.OrderStatusValue);
                return NotFound("Order status not found.");
            }

            try
            {
                int? orderId = await _orderService.UpdateOrderStatusAsync(updateOrderModel);

                if (orderId == null)
                {
                    _logger.LogWarning("Order with ID {OrderId} not found.", updateOrderModel.OrderId);
                    return NotFound("Order not found.");
                }

                _logger.LogInformation("Order status updated successfully for order ID {OrderId}.", updateOrderModel.OrderId);
                return Ok("Order status updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the order status for order ID {OrderId}.", updateOrderModel.OrderId);
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id < 0)
            {
                _logger.LogWarning("Invalid order ID: {Id}", id);
                return BadRequest("Invalid order ID.");
            }

            try
            {
                var order = await _orderService.GetByIdAsync(id);

                if (order == null)
                {
                    _logger.LogInformation("Order with ID {Id} not found.", id);
                    return NotFound($"Order with ID {id} not found.");
                }

                _logger.LogInformation("Order with ID {Id} retrieved successfully.", id);
                return Ok(order);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the order with ID {Id}.", id);
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpGet]
        async public Task<IActionResult> GetCurrentUserOrders()
        {
            string currentUserEmail = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email) ?? string.Empty;

            if (string.IsNullOrWhiteSpace(currentUserEmail))
            {
                _logger.LogWarning("Unauthorized access attempt with no email found in claims.");
                return Unauthorized();
            }

            try
            {
                var orders = await _orderService.GetAllAsync(currentUserEmail);

                if (orders == null || !orders.Any())
                {
                    _logger.LogInformation("No orders found for user with email {Email}.", currentUserEmail);
                    return NotFound("No orders found.");
                }

                _logger.LogInformation("Orders retrieved successfully for user with email {Email}.", currentUserEmail);
                return Ok(orders);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving orders for the current user.");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpGet("all")]
        [Authorize(Roles = "Admin")]
        async public Task<IActionResult> GetAllOrders()
        {
            try
            {
                var orders = await _orderService.GetAllAsync();

                if (orders == null || !orders.Any())
                {
                    _logger.LogInformation("No orders found.");
                    return NotFound("No orders found.");
                }

                _logger.LogInformation("Orders retrieved successfully.");
                return Ok(orders);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all orders.");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request.");
            }
        }

        private static bool CheckIfOrderStatusExists(int statusId)
        {
            return Enum.IsDefined(typeof(OrderStatus), statusId);
        }
    }
}
