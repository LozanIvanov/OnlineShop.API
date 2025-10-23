using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Application.Services;
using OnlineShop.Application.DTOs;
using System;
using System.Linq;
using System.Security.Claims;



namespace OnlineShop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly CartService _service;

        public CartController(CartService service)
        {
            _service = service;
        }

    
        [HttpGet]
        public IActionResult GetCart()
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
            var cartItems = _service.GetCartForUser(userId);
            return Ok(cartItems);
        }

        [HttpPost("{productId}")]
        public IActionResult AddToCart(Guid productId, [FromQuery] int quantity = 1)
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
            _service.AddItem(userId, productId, quantity);
            return Ok("Item added to cart.");
        }

        [HttpDelete("{productId}")]
        public IActionResult RemoveFromCart(Guid productId)
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
            _service.RemoveItem(userId, productId);
            return Ok("Item removed from cart.");
        }
    }
}
