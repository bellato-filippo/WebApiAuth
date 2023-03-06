using Auth.Models;
using Auth.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IJwtManager _jwtManager;
        public UserController(IJwtManager jwtManager)
        {
            _jwtManager = jwtManager;
        }

        [HttpGet("list")]
        public List<string> Get()
        {
            var users = new List<string>()
            {
                "Brombe",
                "Tullio",
                "Felix"
            };
            return users;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate(User user)
        {
            var token = _jwtManager.Authenticate(user);

            if (token == null)
                return Unauthorized();
            return Ok(token);
        }
    }
}
