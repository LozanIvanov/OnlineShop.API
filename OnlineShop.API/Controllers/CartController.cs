using Microsoft.AspNetCore.Mvc;
using OnlineShop.Application.Services;
using OnlineShop.Domain.Entities;

namespace OnlineShop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly CartService _service;

        public CartController(CartService service)
        {
            _service = service;
        }
        [HttpGet("{userId}")]
        public IActionResult GetCart(Guid userId)
        {
            return Ok(_service.GetCartForUser(userId));
        }

        [HttpPost("{userId}/{productId}")]
        public IActionResult AddItem(Guid userId, Guid productId, [FromQuery] int quantity = 1)
        {
            _service.AddItem(userId, productId, quantity);
            return Ok("Item added to cart");
        }

        [HttpDelete("{userId}/{productId}")]
        public IActionResult RemoveItem(Guid userId, Guid productId)
        {
            _service.RemoveItem(userId, productId);
            return Ok("Item removed from cart");
        }

    }
}
