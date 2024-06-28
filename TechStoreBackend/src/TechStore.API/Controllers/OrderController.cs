using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestSharp.Authenticators;
using RestSharp;
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
        private readonly IConfiguration _configuration;
        private readonly ILogger<OrderController> _logger;

        public OrderController(IOrderService orderService, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, ILogger<OrderController> logger)
        {
            _orderService = orderService;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
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

                var emailBody =
                     $"<h1 style=\"margin-bottom: 10px;\">Order Details</h1>" +
                     $"<p style=\"margin-bottom: 40px;\">Thank you for your order! Below are your order details:</p>" +
                     $"<p><strong>Order ID:</strong> {order.Id}</p>" +
                     $"<p><strong>Customer Name:</strong> {order.FirstName} {order.LastName}</p>" +
                     $"<p><strong>Email:</strong> {order.Email}</p>" +
                     $"<p><strong>Contact Number:</strong> {order.ContactNumber}</p>" +
                     $"<p><strong>Shipping Address:</strong> {order.ShippingAddress}, {order.City}, {order.Country} - {order.ZipCode}</p>" +
                     $"<p><strong>Order Status:</strong> {order.Status}</p>" +
                     $"<p><strong>Products:</strong></p>" +
                     $"<ul>{string.Join("", order.Products.Select(p => $"<li>{p.Product.Name} (Quantity: {p.Quantity}, Price: {p.Product.Price:C})</li>"))}</ul>" +
                     $"<p><strong>Delivery Address:</strong> {order.DeliveryAddress?.ShippingAddress}, {order.DeliveryAddress?.City}, {order.DeliveryAddress?.Country} - {order.DeliveryAddress?.ZipCode}</p>" +
                     $"<p style=\"margin-top: 40px;\">If you have any questions or need further assistance, please contact our customer support.</p>" +
                     $"<p>Thank you for shopping with us!<br/><br/>Best regards,<br/>TechPlanet Team</p>";
                
                var emailSent = SendEmail(emailBody, order.Email);

                if (emailSent)
                {
                    _logger.LogInformation("User with email {Email} changed their password successfully.", order.Email);
                    return Ok("Please, check your email. Order information is sent to your email address.");
                }

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

        private Boolean SendEmail(string body, string email)
        {
            var options = new RestClientOptions()
            {
                BaseUrl = new Uri("https://api.mailgun.net/v3"),
                Authenticator = new HttpBasicAuthenticator("api", _configuration.GetSection("EmailConfig:API_KEY").Value)
            };
            var client = new RestClient(options);
            var request = new RestRequest();

            request.AddParameter("domain", "sandbox582822b6660543f09628c673c33be7b1.mailgun.org", ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", "Mailgun Sandbox <mailgun@sandbox582822b6660543f09628c673c33be7b1.mailgun.org>");
            request.AddParameter("to", email);
            request.AddParameter("subject", "Tech Planet - Order Details");
            request.AddParameter("html", body);
            request.Method = Method.Post;

            var response = client.Execute(request);

            return response.IsSuccessful;
        }
    }
}
