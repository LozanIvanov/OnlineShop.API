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
            var userId = GetUserIdFromToken();
            var cart = _service.GetCartForUser(userId)
                               .Select(ci => new CartItemResponse
                               {
                                   ProductId = ci.ProductId,
                                   ProductName = ci.ProductName,
                                   Price = ci.Price,
                                   Quantity = ci.Quantity
                               });
            return Ok(cart);
        }

        [HttpPost]
        public IActionResult AddItem([FromBody] AddCartItemRequest request)
        {
            var userId = GetUserIdFromToken();
            _service.AddItem(userId, request.ProductId, request.Quantity);
            return Ok("Item added to cart");
        }

        [HttpDelete("{productId}")]
        public IActionResult RemoveItem(Guid productId)
        {
            var userId = GetUserIdFromToken();
            _service.RemoveItem(userId, productId);
            return Ok("Item removed from cart");
        }

        private Guid GetUserIdFromToken()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier || c.Type == System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub);
            if (userIdClaim == null)
                throw new Exception("UserId not found in token");
            return Guid.Parse(userIdClaim.Value);
        }
    }
}
