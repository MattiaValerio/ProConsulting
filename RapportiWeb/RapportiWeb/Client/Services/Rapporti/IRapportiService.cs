using RapportiWeb.Shared;

namespace RapportiWeb.Client.Services.Rapporti
{
    public interface IRapportiService
    {
        public List<Rapporto> ListaRapporti { get; set; }

        public Task CreateRapporto(Rapporto rapporto);
        public Task GenerateRapporto(Richiesta ric, Rapporto rap);
        public Task<List<Rapporto>> GetRapporti();
        public Task UpdateRapporto(Rapporto rapporto, int id);
        public Task DeleteRapporto(int id);

        //permette di ottenere i rapporti di un cliente in base alla ragione sociale
        Task<List<Rapporto>> GetRapportiByRagSoc(string ragsoc);

        Task<List<Rapporto>> RicercaPerData(DateTime? start, DateTime? end);

    }
}
