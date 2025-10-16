using Microsoft.AspNetCore.Mvc;
using OnlineShop.Application.Services;
using OnlineShop.Domain.Entities;

namespace OnlineShop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _service;
        public UserController(UserService service) 
        {
            _service = service;
        }
        [HttpGet]
        public ActionResult GetAll() =>Ok(_service.GetAllUsers());
        [HttpPost]
        public ActionResult AddUser(User user)
        {
            _service.AddUser(user);
            return Ok(user);
        }
       
                
    }
}
