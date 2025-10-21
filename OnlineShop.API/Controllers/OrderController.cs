using Microsoft.AspNetCore.Mvc;
using OnlineShop.Application.Services;
using OnlineShop.Domain.Entities;
using System.Security.Claims;

namespace OnlineShop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }
       private Guid GetUserId() =>
            Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        [HttpGet]
        public IActionResult GetOrders()
        {
            var userId = GetUserId();
            var orders = _orderService.GetOrdersByUser(userId);
            return Ok(orders);
        }

        [HttpPost]
        public IActionResult CreateOrder()
        {
            var userId = GetUserId();
            var order = _orderService.CreateOrder(userId);
            return Ok(order);
        }

    }
}
