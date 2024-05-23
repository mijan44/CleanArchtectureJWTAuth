using Application.Contracts;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : Controller
    {
        private readonly IUser user;

        public AdminController(IUser user)
        {
            this.user = user;
        }

        [HttpGet]
        [Route("Admins")]
        [Authorize(Roles ="Admin")]
        public IActionResult GetAdmin()
        {
            var currentUser = GetCurrentUser();
            return Ok($"This is {currentUser.Role}");
        }

        private ApplicationUser GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if(identity != null)
            {
                var userClaims = identity.Claims;
                return new ApplicationUser
                {
                    Name = userClaims.FirstOrDefault(x=>x.Type==ClaimTypes.NameIdentifier)?.Value,
                    Role= userClaims.FirstOrDefault(x=>x.Type==ClaimTypes.Role)?.Value
                };

            }
            return null;
        }
    }
}
