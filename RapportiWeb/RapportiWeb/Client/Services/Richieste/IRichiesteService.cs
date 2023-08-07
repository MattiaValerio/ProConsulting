using RapportiWeb.Shared;

namespace RapportiWeb.Client.Services.Richieste
{
    public interface IRichiesteService
    {
        List<Richiesta> ListaRichieste { get; set; }
        List<Richiesta> SearchedRichieste { get; set; }
        List<Richiesta> Filter { get; set; }
        
        Task<List<Richiesta>> GetRichieste();

        Task<Richiesta?> GetRichiestaById(int id);

        Task CreateRichiesta(Richiesta Richiesta);

        Task UpdateRichiesta(int id, Richiesta Richiesta);

        Task SearchRichieste(string query);

        Task<List<Richiesta>> GetFilteredRichieste(DateTime startDateFilter, DateTime endDateFilter);
        
        Task DeleteRichiesta(int id);
    }
}
