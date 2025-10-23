using Microsoft.AspNetCore.Mvc;
using OnlineShop.Application.Services;
using OnlineShop.Domain.Entities;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;


namespace OnlineShop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] 
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public IActionResult GetMyOrders()
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
            var orders = _orderService.GetOrdersByUser(userId);
            return Ok(orders);
        }

        [HttpPost]
        public IActionResult CreateOrder()
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
            var order = _orderService.CreateOrder(userId);
            return Ok(order);
        }
    }
}
