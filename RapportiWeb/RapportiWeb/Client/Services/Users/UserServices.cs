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

        public async Task DeleteUser(User user)
        {
            var req = await _http.DeleteAsync($"api/users/{user.Id}");
        }

        public async Task<ServiceResponse<List<User>>> GetUsers()
        {
            var users = await _http.GetFromJsonAsync<ServiceResponse<List<User>>>("api/users");
            return users;
        }

        public async Task<ServiceResponse<User>> UpdateUser(User user)
        {
            await _http.PutAsJsonAsync("api/users", user);

            return new ServiceResponse<User>()
            {
                Success = true,
                Data = user,
                Message = $"L'utente {user.UserName} è stato modificato con successo."
            };
        }
    }
}
