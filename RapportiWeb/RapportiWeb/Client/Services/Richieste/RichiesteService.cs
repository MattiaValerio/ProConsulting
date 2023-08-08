using RapportiWeb.Client.Services.Clienti;
using RapportiWeb.Shared;
using System.Net.Http.Json;

namespace RapportiWeb.Client.Services.Richieste
{
    public class RichiesteService : IRichiesteService
    {
        private readonly HttpClient _http;
        public RichiesteService(HttpClient http)
        {
            _http = http;
        }


        public List<Richiesta> ListaRichieste { get; set; }
        public List<Richiesta> SearchedRichieste { get; set; }
        public List<Richiesta> Filter { get; set; }
        public string[] RagioniSociali { get; set; }


        public Task CreateRichiesta(Richiesta Richiesta)
        {
            throw new NotImplementedException();
        }

        public Task DeleteRichiesta(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Richiesta>> GetFilteredRichieste(DateTime startDateFilter, DateTime endDateFilter)
        {
            throw new NotImplementedException();
        }

        public async Task<string[]> GetRagioniSociali()
        {
            List<string> NomiClienti = new List<string>();

            var res = await _http.GetFromJsonAsync<List<Cliente>>("/api/clienti");


            res.ForEach(c =>
            {
                NomiClienti.Add(c.ragioneSociale);
            });

            RagioniSociali = NomiClienti.ToArray();

            return RagioniSociali;
        }

        public Task<Richiesta?> GetRichiestaById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Richiesta>> GetRichieste()
        {
            throw new NotImplementedException();
        }

        public Task SearchRichieste(string query)
        {
            throw new NotImplementedException();
        }

        public Task UpdateRichiesta(int id, Richiesta Richiesta)
        {
            throw new NotImplementedException();
        }
    }
}
