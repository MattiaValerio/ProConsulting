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
    }
}
