using RapportiWeb.Shared;

namespace RapportiWeb.Client.Services.Users
{
    public interface IUsersServices
    {
        Task<ServiceResponse<List<User>>> GetUsers();
        Task<ServiceResponse<User>> UpdateUser(User user);

        Task DeleteUser(User user);
    }
}
