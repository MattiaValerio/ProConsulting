using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RapportiWeb.Server.Data;
using RapportiWeb.Shared;

namespace RapportiWeb.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientiController : ControllerBase
    {
        private readonly DataContext _context;

        public ClientiController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Cliente>>> GetClienti()
        {
            var clienti = await _context.Clienti
                .Include(x => x.Richieste)
                .Include(x => x.Rapporti)
                .Include(x => x.Utenti)
                .ToListAsync();

            return Ok(clienti);
        }

		[HttpGet]
		[Route("ragsoc")]
		public async Task<ActionResult<string>> GetRagioniSociali()
		{
			List<string> NomiClienti = new List<string>();

			var res = await _context.Clienti.ToListAsync();

			res.ForEach(c =>
			{
				NomiClienti.Add(c.ragioneSociale);
			});

			return Ok(NomiClienti);
		}

        [HttpGet("{ragsoc}")]
        public async Task<ActionResult<Cliente>> GetCliente(string ragsoc)
		{

			var res = await _context.Clienti.ToListAsync();

            var cliente = res.FirstOrDefault(c => c.ragioneSociale == ragsoc);

            return Ok(cliente);
		}




		[HttpPost]
        public async Task<ActionResult<Cliente>> CreateCliente(Cliente cliente)
        {
            _context.Clienti.Add(cliente);

            await _context.SaveChangesAsync(); //salvo i cambiamenti che effettuo nel DB

            return Ok(cliente);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Cliente>> DeleteCliente(int id)
        {
            var dbCliente = _context.Clienti.FirstOrDefault(c => c.id == id);

            if (dbCliente == null)
                return NotFound("CLIENTE NON TROVATO");

            _context.Clienti.Remove(dbCliente);

            await _context.SaveChangesAsync();

            return Ok(dbCliente);

        }

        [HttpGet("search")]
        public async Task<ActionResult<List<Cliente>>> SearchClienti([FromQuery] string query)
        {
            // Fetch all clients from the database
            var clienti = await _context.Clienti.ToListAsync();

            // If a search query is provided, apply the filter
            if (!string.IsNullOrEmpty(query))
            {
                query = query.ToLower();

                clienti = clienti.Where(c => c.ragioneSociale.ToLower().Contains(query)).ToList();
            }

            return Ok(clienti);
        }


        [HttpPut]
        public async Task<ActionResult<Cliente>> UpdateCliente(Cliente cliente)
        {
            var dbCliente = _context.Clienti.FirstOrDefault(c => c.id == cliente.id);

            if (dbCliente == null)
                return NotFound("CLIENTE NON TROVATO");

            dbCliente.ragioneSociale = cliente.ragioneSociale;
            dbCliente.email = cliente.email;
            dbCliente.Citta = cliente.Citta;
            dbCliente.Indirizzo = cliente.Indirizzo;
            dbCliente.Provincia = cliente.Provincia;
            dbCliente.Stato = cliente.Stato;
            dbCliente.telefono = cliente.telefono;
            dbCliente.CAP = cliente.CAP;
            dbCliente.email = cliente.email;

            await _context.SaveChangesAsync();

            return Ok(dbCliente);

        }
    }
}
