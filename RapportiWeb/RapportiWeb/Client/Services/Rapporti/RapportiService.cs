using Microsoft.AspNetCore.Components;
using RapportiWeb.Shared;
using System.Net.Http.Json;

namespace RapportiWeb.Client.Services.Rapporti
{
    public class RapportiService : IRapportiService
    {
        private readonly HttpClient _http;
        private NavigationManager _NavigationManager;

        public RapportiService(HttpClient http, NavigationManager nav)
        {
            _http = http;
            _NavigationManager = nav;
        }

        public List<Rapporto> ListaRapporti { get; set; } = new List<Rapporto>();

        public async Task CreateRapporto(Rapporto rapporto)
        {
            await _http.PostAsJsonAsync("/api/rapporti", rapporto);
        }

        public async Task DeleteRapporto(int id)
        {
            await _http.DeleteAsync($"api/rapporti/{id}");
        }

		public async Task GenerateRapporto(Richiesta ric, Rapporto rap)
		{
            var postRap = await _http.PostAsJsonAsync("/api/rapporti", rap);

            if (postRap.IsSuccessStatusCode)
            {
                ric.RapportoCreato = true;
                await _http.PutAsJsonAsync("/api/richieste", ric);
            }
		}

		public async Task<List<Rapporto>> GetRapporti()
        {
            var req =  await _http.GetFromJsonAsync<List<Rapporto>>("api/rapporti");

            if (req != null)
            {
                ListaRapporti = req;
                return req;
            }
            else
            {
                return new List<Rapporto>();
            }

        }

        public async Task<List<Rapporto>> GetRapportiByRagSoc(string ragsoc)
        {
            var result = await _http.GetFromJsonAsync<List<Rapporto>>($"api/rapporti/{ragsoc}");

            if (result == null)
                return new List<Rapporto>();

            return result.ToList();
        }

        public async Task UpdateRapporto(Rapporto rapporto, int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Rapporto>> RicercaPerData(DateTime? start, DateTime? end)
        {

            var s = start?.ToString("yyyy-MM-ddTHH:mm:ss");
            var e = end?.ToString("yyyy-MM-ddTHH:mm:ss");

            var rapporti = await _http.GetFromJsonAsync<List<Rapporto>>($"/api/rapporti/data?start={s}&end={e}");

            if (rapporti != null)
                return rapporti.ToList();

            return new List<Rapporto>();
        }

        public async Task<Rapporto> GetRapportoByRichiesta(Richiesta ric)
        {
            try
            {
                var rap = await _http.GetFromJsonAsync<List<Rapporto>>($"/api/rapporti/ric?RichiestaId={ric.id}");

                if (rap is not null)
                    return rap.FirstOrDefault();

                return new Rapporto();
            }catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new Rapporto();
            }

        }
    }
}
