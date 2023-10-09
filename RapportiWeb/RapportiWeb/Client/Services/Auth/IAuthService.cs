using RapportiWeb.Shared;

namespace RapportiWeb.Client.Services.Auth
{
    public interface IAuthService
    {
        Task<ServiceResponse<int>> Register(UserRegistration request, Cliente cliente);
        Task<ServiceResponse<string>> Login(UserLogin request);
    }
}
