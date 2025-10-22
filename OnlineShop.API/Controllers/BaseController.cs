using Microsoft.AspNetCore.Mvc;

namespace OnlineShop.API.Controllers
{
    public class BaseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
