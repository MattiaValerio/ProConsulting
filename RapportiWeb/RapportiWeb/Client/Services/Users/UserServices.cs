using RapportiWeb.Shared;
using System.Net.Http;
using System.Net.Http.Json;

namespace RapportiWeb.Client.Services.Users
{
    public class UserServices :IUsersServices
    {
        private HttpClient _http;

        public UserServices(HttpClient http)
        {
            _http = http;
        }

        public async Task<ServiceResponse<List<User>>> GetUsers()
        {
            var users = await _http.GetFromJsonAsync<ServiceResponse<List<User>>>("api/users");
            return users;
        }


    }
}
