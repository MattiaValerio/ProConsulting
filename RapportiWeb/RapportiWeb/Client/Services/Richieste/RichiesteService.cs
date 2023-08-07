using RapportiWeb.Shared;

namespace RapportiWeb.Client.Services.Richieste
{
    public class RichiesteService : IRichiesteService
    {
        public List<Richiesta> ListaRichieste { get; set; }
        public List<Richiesta> SearchedRichieste { get; set; }
        public List<Richiesta> Filter { get; set; }

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
