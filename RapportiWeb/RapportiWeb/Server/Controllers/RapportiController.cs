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
        public async Task<ActionResult<Rapporto>> CreateRichiesta(Rapporto rapporto)
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
            var dbRapporti = _context.Rapporti.FirstOrDefault(c => c.id == id);

            if (dbRapporti == null)
                return NotFound("RAPPORTO NON TROVATO");

            _context.Rapporti.Remove(dbRapporti);

            await _context.SaveChangesAsync(); //salvo i cambiamenti che effettuo nel DB

            return Ok(dbRapporti);

        }
    }
}
