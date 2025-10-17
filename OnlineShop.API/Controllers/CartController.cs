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

        [HttpGet]
        public IActionResult GetAll() => Ok(_service.GetAllItems());

        [HttpPost]
        public IActionResult Add(CartItem item)
        {
            _service.AddItem(item);
            return Ok(item);
        }

        [HttpDelete("{id}")]
        public IActionResult Remove(Guid id)
        {
            _service.RemoveItem(id);
            return Ok();
        }
    }
}
