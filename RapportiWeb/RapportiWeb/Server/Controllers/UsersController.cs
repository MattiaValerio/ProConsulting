using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RapportiWeb.Server.Data;
using RapportiWeb.Server.Services.AuthService;
using RapportiWeb.Shared;

namespace RapportiWeb.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly DataContext _context;

        public UsersController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegistration req)
        {
            var res = await _authService.Register(
                new User
                {
                    UserName = req.UserName,
                    Nome = req.Nome,
                    Cognome = req.Cognome,
                    ClienteId = req.ClienteId,
                    Amministratore = req.Amministratore,
                    Attivo = req.Attivo,
                    Figuraprof = req.Figuraprof,
                    TipoUtente = req.TipoUtente
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
