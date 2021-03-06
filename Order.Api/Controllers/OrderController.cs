using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Order.Events;
using Order.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost]
        public async Task<OrderDto> CreateOrder(OrderType orderType, List<OrderItemDto> orderItems)
        {

            return await _orderService.CreateOrder(orderType, orderItems);
        }

        [HttpPost]
        public async Task<ActionResult> ChangeProductPrice(Guid productId, double price)
        {
            await _orderService.ChangeProductPrice(productId, price);
            return Ok();
        }
    }
}
