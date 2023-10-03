using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
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


		public async Task DownloadRichiesta(Cliente cliente, Richiesta ric)
		{
			try
			{
				var response = await _http.PostAsJsonAsync("api/PDF/DownloadRichiesta", ric);
				if (response.IsSuccessStatusCode)
				{
					var pdfFileName = await response.Content.ReadAsStringAsync();
					var pdfUrl = $"../PDF/{pdfFileName}";

				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
			
		}

		public async Task GetFile(string filename)
		{
			await _http.GetAsync($"pdf/{filename}");
		}
	}
}