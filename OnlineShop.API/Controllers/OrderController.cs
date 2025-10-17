using Microsoft.AspNetCore.Mvc;
using OnlineShop.Application.Services;
using OnlineShop.Domain.Entities;

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
        [HttpGet]
        public IActionResult GetAll()=>Ok(_orderService.GetAllOrders());    

        [HttpPost]

        public IActionResult Create (Order order)
        {
            _orderService.AddOrder(order);

            return Ok(order);
        }
        
    }
}
