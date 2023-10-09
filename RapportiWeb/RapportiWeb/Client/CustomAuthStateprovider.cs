using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Claims;
using System.Text.Json;

namespace RapportiWeb.Client
{
    public class CustomAuthStateprovider : AuthenticationStateProvider
    {
        private ILocalStorageService _localStorage;
        private HttpClient _http;

        public CustomAuthStateprovider(ILocalStorageService localStorage, HttpClient http)
        {
            _localStorage = localStorage;
            _http = http;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            string authtoken = await _localStorage.GetItemAsStringAsync("authToken");

            var identity = new ClaimsIdentity();
            _http.DefaultRequestHeaders.Authorization = null;

            if (!string.IsNullOrEmpty(authtoken))
            {
                try
                {
                    identity = new ClaimsIdentity(ParseClaimsFromJwt(authtoken), "jwt");
                    _http.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", authtoken.Replace("\"", ""));
                }
                catch
                {
                    await _localStorage.RemoveItemAsync(authtoken);
                    identity = new ClaimsIdentity();
                }
            }

            var user = new ClaimsPrincipal(identity);
            var state = new AuthenticationState(user);

            NotifyAuthenticationStateChanged(Task.FromResult(state));

            return state;

        }

        private byte[] ParseClaimsWhitoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }

        private IEnumerable<Claim>? ParseClaimsFromJwt(string jwt)
        {
            var payload = jwt.Split('.')[1];
            var jsonbytes = ParseClaimsWhitoutPadding(payload);

            var keyvaluepairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonbytes);

            var claims = keyvaluepairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));

            return claims;
        }

        


    }
}
