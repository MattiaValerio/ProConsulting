﻿using RapportiWeb.Shared;

namespace RapportiWeb.Server.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResponse<int>> Register(User user, string password);
        Task<bool> UserExists(string username);

        Task<ServiceResponse<string>> Login(string username, string password);
    }
}
