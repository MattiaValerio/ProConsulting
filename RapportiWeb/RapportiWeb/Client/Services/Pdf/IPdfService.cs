using RapportiWeb.Shared;

namespace RapportiWeb.Client.Services.Pdf
{
	public interface IPdfService
	{
        Task<string> DownloadRichiesta(Cliente cliente, Richiesta ric);
        Task<string> DownloadRapporto(Cliente cliente, Rapporto rap);

        Task GetFile(string filename);
	}
}
