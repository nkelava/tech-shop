﻿using AutoMapper;
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
        public async Task<IActionResult> Add([FromBody] OrderCreateModel order)
        {
            if (order == null)
                return BadRequest();

            await _orderService.AddAsync(order);

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
            if (order == null)
                return BadRequest();

            await _orderService.UpdateAsync(order);

            return Ok();
        }

        [HttpGet]
        public IActionResult GetOrders(string? email)
        {
            var orders = (email == null) ? _orderService.GetOrders() : _orderService.GetOrders(email);

            return Ok(orders);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id < 0)
                return BadRequest();

            var order =  _orderService.GetOrderById(id);

            if (order == null)
                return NotFound();

            return Ok(order);
        }

        //[HttpGet("{email}")]
        //public async Task<IActionResult> GetByEmail(string email)
        //{
        //    if (email == null || email.Length == 0)
        //        return BadRequest();

        //    var orders = _orderService.GetOrdersByEmail(email);

        //    if (orders == null)
        //        return NotFound();

        //    return Ok(orders);
        //}
    }
}
