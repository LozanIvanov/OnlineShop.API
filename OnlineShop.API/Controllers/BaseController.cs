using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;

namespace OnlineShop.API.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected Guid GetUserIdFromToken()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
                throw new Exception("User ID not found in token.");

            return Guid.Parse(userIdClaim);
        }
    }
}

