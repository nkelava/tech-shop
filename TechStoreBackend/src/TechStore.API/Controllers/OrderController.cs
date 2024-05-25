using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public readonly IOrderService _orderService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public readonly IMapper _mapper;

        public OrderController(IOrderService orderService, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _orderService = orderService;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderCreateModel order)
        {
            if (order is null)
                return BadRequest();

            if (order.Products.Count < 1)
                return BadRequest();

            try
            {
                await _orderService.CreateAsync(order);
                return Ok(order);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("{orderId:int}")]
        public async Task<IActionResult> Delete(int orderId)
        {
            if (orderId < 1)
                return BadRequest();

            int deletedOrderId = await _orderService.DeleteAsync(orderId);

            return orderId < 1 ? NotFound() : Ok(deletedOrderId);
        }

        [HttpPut("status")]
        public async Task<IActionResult> UpdateOrderStatus(OrderUpdateStatusModel updateOrderModel)
        {
            if (CheckIfOrderStatusExists(updateOrderModel.OrderStatusValue) == false)
                return NotFound();

            int orderId = await _orderService.UpdateOrderStatusAsync(updateOrderModel);

            return (orderId < 1) ? NotFound() : Ok();
        }

        [HttpGet]
        async public Task<IActionResult> GetOrders()
        {
            string currentUserEmail = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email) ?? "";

            if (string.IsNullOrWhiteSpace(currentUserEmail))
                return Unauthorized();

            var orders = await _orderService.GetOrdersAsync(currentUserEmail);

            return (orders is null) ? NotFound() : Ok(orders);
        }

        [HttpGet("all")]
        async public Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderService.GetOrdersAsync();
            return Ok(orders);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id < 0)
                return BadRequest();

            var order =  await _orderService.GetOrderByIdAsync(id);

            return (order is null) ? NotFound() : Ok(order);
        }

        private static bool CheckIfOrderStatusExists(int statusId)
        {
            return Enum.IsDefined(typeof(OrderStatus), statusId);
        }

        //[HttpGet("{email:string}")]
        //public async Task<IActionResult> GetByEmail(string email)
        //{
        //    if (string.IsNullOrWhiteSpace(email))
        //        return BadRequest();

        //    var orders = await _orderService.GetOrdersAsync(email);

        //    return (orders is null) ? NotFound() : Ok(orders);
        //}
    }
}
