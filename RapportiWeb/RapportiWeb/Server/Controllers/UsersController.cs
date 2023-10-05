using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RapportiWeb.Server.Services.AuthService;
using RapportiWeb.Shared;

namespace RapportiWeb.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IAuthService _authService;

        public UsersController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegistration req, string password)
        {
            var res = await _authService.Register(
                new User
                {
                    UserName = req.UserName
                },
                req.Password);

            if(res.Success == false)
            {
                return BadRequest(res);
            }

            return Ok(res);
        }
    }
}
