using Microsoft.AspNetCore.Components;
using RapportiWeb.Shared;
using System.Net.Http.Json;

namespace RapportiWeb.Client.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _http;
        private NavigationManager _NavigationManager;

        public AuthService(HttpClient http, NavigationManager nav)
        {
            _http = http;
            _NavigationManager = nav;
        }

        public async Task<ServiceResponse<int>> Register(UserRegistration req, Cliente cliente)
        {
            req.ClienteId = cliente.id;
            var result = await _http.PostAsJsonAsync("api/users/register", req);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<int>>();
        }
    }
}
