using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RapportiWeb.Server.Data;
using RapportiWeb.Shared;

namespace RapportiWeb.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RapportiController : ControllerBase
    {
        private readonly DataContext _context;

        public RapportiController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Rapporto>> CreateRapporto(Rapporto rapporto)
        {
            _context.Rapporti.Add(rapporto);

            await _context.SaveChangesAsync(); //salvo i cambiamenti che effettuo nel DB

            return Ok(rapporto);
        }

        [HttpGet]
        public async Task<ActionResult<List<Rapporto>>> GetRapporti()
        {
            var rapporti = await _context.Rapporti.ToListAsync();

            return Ok(rapporti.OrderByDescending(r => r.dataCreazioneRapporto));
        }

        [HttpPut]
        public async Task<ActionResult<Rapporto>> UpdateRapporto(Rapporto rapporto)
        {
            var dbRapporto = _context.Rapporti.FirstOrDefault(c => c.id == rapporto.id);

            if (dbRapporto == null)
                return NotFound("RAPPORTO NON TROVATO");

            dbRapporto = rapporto;

            await _context.SaveChangesAsync(); //salvo i cambiamenti che effettuo nel DB

            return Ok(rapporto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Rapporto>> DeleteRapporto(int id)
        {
            var dbRapporto = _context.Rapporti.FirstOrDefault(c => c.id == id);

            if (dbRapporto == null)
                return NotFound("RAPPORTO NON TROVATO");

            _context.Rapporti.Remove(dbRapporto);

            var richiesta = _context.Richieste.FirstOrDefault(richiesta => richiesta.id == dbRapporto.RichiestaId) ;

            if(richiesta != null)
            {
                richiesta.RapportoCreato = false;
            }

            await _context.SaveChangesAsync(); //salvo i cambiamenti che effettuo nel DB

            return Ok(dbRapporto);

        }

        [HttpGet("{ragsoc}")]
        public async Task<ActionResult<List<Rapporto>>> RapportiByRagSoc(string ragsoc)
        {
            var clienti = await _context.Clienti.ToListAsync();
            var rapporti = await _context.Rapporti.ToListAsync();

            var cliente = clienti.Where(c => c.ragioneSociale == ragsoc).FirstOrDefault();

            if (cliente != null)
                rapporti = rapporti.Where(r => r.Clienteid == cliente.id).ToList();

            return Ok(rapporti.OrderByDescending(r => r.dataCreazioneRapporto));
        }

        [HttpGet("data")]
        public async Task<List<Rapporto>> FiltraRapportiByDate([System.Web.Http.FromUri] DateTime start, [System.Web.Http.FromUri] DateTime end)
        {
            var richieste = _context.Rapporti.ToList();

            if (richieste != null)
            {
                var filteredRapporti = richieste.Where(r => r.dataCreazioneRapporto >= start && r.dataCreazioneRapporto <= end).ToList();
                return filteredRapporti;
            }

            return new List<Rapporto>();
        }


        [HttpGet("ric")]
        public async Task <ActionResult<List<Rapporto>>> GetRapportoByRichiesta([System.Web.Http.FromUri] int RichiestaId)
        {
            var richieste = await _context.Richieste.ToListAsync();

            var rapporti = await _context.Rapporti.ToListAsync();

            var rapporto = rapporti.Where(rap => rap.RichiestaId == RichiestaId).ToList();

            return Ok(rapporto);

        }



    }
}
