using Microsoft.AspNetCore.Components;
using RapportiWeb.Shared;
using System.Net.Http.Json;

namespace RapportiWeb.Client.Services.Clienti
{
    public class ClientiService : IClientiService
    {
        private readonly HttpClient _http;
        private NavigationManager _NavigationManager;

        public ClientiService(HttpClient http, NavigationManager nav)
        {
            _http = http;
            _NavigationManager = nav;
        }


        public List<Cliente> ListaClienti { get; set; } = new List<Cliente>();
        public List<Cliente> SearchedClienti { get; set; } = new List<Cliente>();

        public async Task CreateCliente(Cliente Cliente)
        {
            await _http.PostAsJsonAsync("/api/clienti", Cliente);
        }

        public async Task DeleteCliente(int Id)
        {
            await _http.DeleteAsync($"api/Clienti/{Id}");
        }

        public async Task<List<Cliente>> GetClienti()
        {
            var result = await _http.GetFromJsonAsync<List<Cliente>>("api/Clienti");

            return result.ToList();
        }

        public async Task<List<Cliente>> SearchClienti(string query)
        {
            var result = await _http.GetFromJsonAsync<List<Cliente>>($"api/Clienti/search?query={query}");

            if (result is not null)
            {
                SearchedClienti = result;
            }

            return SearchedClienti;
        }

        public async Task UpdateCliente(Cliente Cliente)
        {
            await _http.PutAsJsonAsync("api/Clienti", Cliente);
        }
    }
}
