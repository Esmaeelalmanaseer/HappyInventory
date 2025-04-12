using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HappyInventory.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DebugTokenController : ControllerBase
    {
        [HttpGet("debug-token")]
        [Authorize]
        public IActionResult DebugToken()
        {
            var isAuth = User.Identity?.IsAuthenticated;
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var roles = User.FindAll(ClaimTypes.Role).Select(r => r.Value).ToList();

            return Ok(new
            {
                isAuth,
                email,
                roles
            });
        }
    }
}
