using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TechStore.Application.Interfaces.Services;
using TechStore.Application.Models.Order;


namespace TechStore.API.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        public readonly IOrderService _orderService;
        public readonly IMapper _mapper;

        public OrderController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderCreateModel order)
        {
            if (order is null)
                return BadRequest();

            await _orderService.CreateAsync(order);

            return Ok(order);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int orderId)
        {
            if (orderId < 1)
                return BadRequest();

            await _orderService.DeleteAsync(orderId);

            return Ok(orderId);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] OrderUpdateModel order)
        {
            if (order is null)
                return BadRequest();

            await _orderService.UpdateAsync(order);

            return Ok();
        }

        [HttpGet]
        public IActionResult GetOrders(string? email)
        {
            var orders = (email is null) ? _orderService.GetOrdersAsync() : _orderService.GetOrdersAsync(email);

            return (email is not null && orders is null) ? NotFound() : Ok(orders);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id < 0)
                return BadRequest();

            var order =  _orderService.GetOrderByIdAsync(id);

            return (order is null) ? NotFound() : Ok(order);
        }

        //[HttpGet("{email}")]
        //public async Task<IActionResult> GetByEmail(string email)
        //{
        //    if (email is null)
        //        return BadRequest();

        //    var orders = _orderService.GetOrdersByEmail(email);

        //    return (orders is null) ? NotFound() : Ok(orders);
        //}
    }
}
