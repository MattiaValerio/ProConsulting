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

        public async Task<ServiceResponse<string>> Login(UserLogin request)
        {
            var req = await _http.PostAsJsonAsync("api/users/login", request);

            var resp = await req.Content.ReadFromJsonAsync<ServiceResponse<string>>();

            if(resp is not null)
            {
                return resp;
            }
            else
            {
                return new ServiceResponse<string>()
                {
                    Success = false,
                    Data = string.Empty,
                    Message = "Errore durante il login"
                };
            }
        }

        public async Task<ServiceResponse<int>> Register(UserRegistration req, Cliente cliente)
        {
            try
            {
                var result = await _http.PostAsJsonAsync("api/users/register", req);
                req.ClienteId = cliente.id;

                return await result.Content.ReadFromJsonAsync<ServiceResponse<int>>();
            }
            catch
            {
                return new ServiceResponse<int>()
                {
                    Data = -1
                };
            }
        }
    }
}
