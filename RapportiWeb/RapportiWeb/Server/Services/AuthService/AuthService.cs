using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RapportiWeb.Server.Data;
using RapportiWeb.Shared;
using System.Security.Cryptography;

namespace RapportiWeb.Server.Services.AuthService
{
    public class AuthService : IAuthService
    {

        public DataContext _context { get; }

        public AuthService(DataContext context)
        {
            _context = context;
        }


        public async Task<ServiceResponse<int>> Register(User user, string password)
        {
            if(await UserExists(user.UserName))
            {
                return new ServiceResponse<int>
                {
                    Success = false,
                    Message = "Utente già registrato"
                };
            }

            CreatePassword(password, out byte[] passwordHash, out byte[] passwordSalt);
            
            user.Salt = passwordSalt;
            user.Hash = passwordHash;

            _context.Add(user);
            await _context.SaveChangesAsync();

            return new ServiceResponse<int>
            {
                Success = true,
                Data = user.Id,
                Message = "Utente creato con successo"
            };
        }

        public async Task<bool> UserExists(string username)
        {
            if (await _context.Utenti.AnyAsync(u=> u.UserName.ToLower() == username.ToLower()))
            {
                return true;
            }
            return false;
        }

        private void CreatePassword(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.
                    ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
