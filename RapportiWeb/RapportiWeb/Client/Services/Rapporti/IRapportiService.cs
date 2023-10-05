using RapportiWeb.Shared;

namespace RapportiWeb.Client.Services.Rapporti
{
    public interface IRapportiService
    {
        public List<Rapporto> ListaRapporti { get; set; }

        public Task CreateRapporto(Rapporto rapporto);
        public Task GenerateRapporto(Richiesta ric, Rapporto rap);
        public Task<List<Rapporto>> GetRapporti();
        public Task UpdateRapporto(Rapporto rapporto);
        public Task DeleteRapporto(int id);

        public Task<Rapporto> GetRapportoByRichiesta(Richiesta ric);

        //permette di ottenere i rapporti di un cliente in base alla ragione sociale
        public Task<List<Rapporto>> GetRapportiByRagSoc(string ragsoc);

        public Task<List<Rapporto>> RicercaPerData(DateTime? start, DateTime? end);

    }
}
