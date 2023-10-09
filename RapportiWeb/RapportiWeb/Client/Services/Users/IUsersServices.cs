using RapportiWeb.Shared;

namespace RapportiWeb.Client.Services.Users
{
    public interface IUsersServices
    {
        Task<ServiceResponse<List<User>>> GetUsers();
    }
}
