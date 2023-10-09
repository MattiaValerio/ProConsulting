using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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

        public UsersController(IAuthService authService, DataContext context)
        {
            _authService = authService;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<User>>>> GetUsers()
        {
            var utenti = await _context.Utenti.ToListAsync<User>();

            if (utenti.IsNullOrEmpty())
            {
                var res = new ServiceResponse<List<User>>()
                {
                    Message = "Errore nel reperire i dati degli utenti ",
                    Success = false
                };
                return BadRequest(res);
            }
            else
            {
                var succres = new ServiceResponse<List<User>>()
                {
                    Success = true,
                    Data = utenti,
                    Message = "Utenti recuperati con successo."
                };
                return Ok(succres);
            }
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

            if (res.Success == false)
            {
                return BadRequest(res);
            }


            return Ok(res);
        }

        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<string>>> Login(UserLogin req)
        {
            var res = await _authService.Login(req.UserName, req.Password);

            if (res.Success == false)
            {
                return BadRequest(res);
            }
            return Ok(res);

        }
    }
}
