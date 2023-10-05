﻿using RapportiWeb.Shared;

namespace RapportiWeb.Client.Services.Pdf
{
	public interface IPdfService
	{
        Task DownloadRichiesta(Cliente cliente, Richiesta ric);
        Task DownloadRapporto(Cliente cliente, Rapporto rap);

        Task GetFile(string filename);
	}
}
