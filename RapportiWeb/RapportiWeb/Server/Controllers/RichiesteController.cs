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

            return Ok(richieste.OrderByDescending(r => r.Data));
        }

        [HttpGet]
        public async Task<ActionResult<List<Richiesta>>> RichiesteByRagSoc(string ragsoc)
        {
            var clienti = await _context.Clienti.ToListAsync();
            var richieste = await _context.Richieste.ToListAsync();

            var ricerca = richieste.Where(r=> r.Clienteid == clienti.FirstOrDefault(c=>))

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
		public async Task<ActionResult<Cliente>> CreateRichiesta(Richiesta richiesta)
		{
			_context.Richieste.Add(richiesta);

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
			var dbRichiesta = _context.Richieste.FirstOrDefault(c => c.id == richiesta.id);

			if (dbRichiesta == null)
				return NotFound("RICHIESTA NON TROVATA");

            dbRichiesta.FiguraProfessionale = richiesta.FiguraProfessionale;
            dbRichiesta.TipologiaIntervento = richiesta.TipologiaIntervento;
            dbRichiesta.ResponsabileRic = richiesta.ResponsabileRic;
            dbRichiesta.DurataIntervento = richiesta.DurataIntervento;
            dbRichiesta.DataIntervento = richiesta.DataIntervento;
            dbRichiesta.Descrizione = richiesta.Descrizione;

			await _context.SaveChangesAsync();

			return Ok(richiesta);

		}
	}
}