using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RapportiWeb.Client.Pages;
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

            return Ok(richieste.OrderByDescending(r => r.Data));
        }

        [HttpGet("{ragsoc}")]
        public async Task<ActionResult<List<Richiesta>>> RichiesteByRagSoc(string ragsoc)
        {
            var clienti = await _context.Clienti.ToListAsync();
            var richieste = await _context.Richieste.ToListAsync();

            var cliente = clienti.Where(c => c.ragioneSociale == ragsoc).FirstOrDefault();

            if(cliente != null)
                richieste = richieste.Where(r => r.Clienteid == cliente.id).ToList();

            return Ok(richieste.OrderByDescending(r => r.Data));
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

		[HttpPost]
		public async Task<ActionResult<Richiesta>> CreateRichiesta(Richiesta richiesta)
		{

            await _context.AddAsync(richiesta); //la richiesta viene aggiunta al DB

			await _context.SaveChangesAsync(); //salvo i cambiamenti che effettuo nel DB

			return Ok(richiesta);
		}

        [HttpDelete("{id}")]
        public async Task<ActionResult<Richiesta>> DeleteRichiesta(int id)
        {
            var dbRichieste = _context.Richieste.FirstOrDefault(c => c.id == id);

            if (dbRichieste == null)
                return NotFound("RICHIESTA NON TROVATA");

            _context.Richieste.Remove(dbRichieste);

            await _context.SaveChangesAsync();

            return Ok(dbRichieste);

        }

		[HttpPut]
		public async Task<ActionResult<Richiesta>> UpdateRichiesta(Richiesta richiesta)
		{
            _context.Update(richiesta);

			await _context.SaveChangesAsync();

			return Ok(richiesta);

		}

        [HttpGet("data")]
        public async Task<List<Richiesta>> FiltraRichiesteByDate([System.Web.Http.FromUri] DateTime start, [System.Web.Http.FromUri] DateTime end)
        {
            var richieste = _context.Richieste.ToList();

            if (richieste != null)
            {
                var filteredRichieste = richieste.Where(r => r.Data.Date >= start.Date && r.Data.Date <= end.Date).ToList();
                return filteredRichieste;
            }

            return new List<Richiesta>();
        }
	}
}