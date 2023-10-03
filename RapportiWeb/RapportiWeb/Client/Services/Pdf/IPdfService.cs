using RapportiWeb.Shared;

namespace RapportiWeb.Client.Services.Pdf
{
	public interface IPdfService
	{
		Task DownloadRichiesta(Richiesta ric);
	}
}
