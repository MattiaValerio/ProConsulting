using Microsoft.AspNetCore.Components;
using RapportiWeb.Shared;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;

namespace RapportiWeb.Client.Services.Pdf
{
	public class PdfService : IPdfService
	{
		private readonly HttpClient _http;
		private NavigationManager _NavigationManager;

		public PdfService(HttpClient http, NavigationManager nav)
		{
			_http = http;
			_NavigationManager = nav;
		}


		public async Task DownloadRichiesta(Richiesta ric)
		{
			var response = await _http.PostAsJsonAsync("api/PDF/DownloadRichiesta", ric);
			if (response.IsSuccessStatusCode)
			{
				var pdfFileName = await response.Content.ReadAsStringAsync();
				var pdfUrl = $"../PDFs/{pdfFileName}";

				using (Process p = new Process())
				{
					var path = Path.GetDirectoryName(pdfUrl);

					p.StartInfo = new ProcessStartInfo()
					{
						CreateNoWindow = true,
						UseShellExecute = true,

						FileName = pdfUrl
					};
					p.Start();
				}
			}
		}
	}
}