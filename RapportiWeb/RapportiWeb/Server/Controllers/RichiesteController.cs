using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RapportiWeb.Server.Data;
using RapportiWeb.Shared;

namespace RapportiWeb.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RichiesteController : ControllerBase
    {
        private readonly DataContext _context;

        public RichiesteController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Richiesta>>> GetRichieste()
        {
            var richieste = await _context.Richieste.ToListAsync();

            return Ok(richieste);
        }

        [HttpGet]
        [Route("ragsoc")]
        public async Task<ActionResult<List<string>>> GetRagioniSociali()
        {
            List<string> NomiClienti = new List<string>();

            var res = await _context.Clienti.ToListAsync();

            res.ForEach(c =>
            {
                NomiClienti.Add(c.ragioneSociale);
            });

            return Ok(NomiClienti);
        }
    }
}