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

        //Esegue la registrazione di un utente
        //Verifica che l'username non sia gia presente in DB e poi genera Hash e Salt per la password
        //Aggiunge l'user al DB e ritorna un ServiceResponse con l'id del nuovo utente
        public async Task<ServiceResponse<int>> Register(User user, string password)
        {
            if(await UserExists(user.UserName))
            {
                return new ServiceResponse<int>
                {
                    Success = false,
                    Message = "Username già esistente"
                };
            }

            CreatePassword(password, out byte[] passwordHash, out byte[] passwordSalt);
            
            user.Salt = passwordSalt;
            user.Hash = passwordHash;

            //await _context.Database.ExecuteSqlAsync ($"SET IDENTITY_INSERT [dbo].[Utenti] ON");

            _context.Add(user);
            await _context.SaveChangesAsync();

            //await _context.Database.ExecuteSqlAsync($"SET IDENTITY_INSERT [dbo].[Utenti] OFF");

            return new ServiceResponse<int>
            {
                Success = true,
                Data = user.Id,
                Message = "Utente creato con successo"
            };
        }

        //Verifica se l'username di un utente è gia stato registrato
        public async Task<bool> UserExists(string username)
        {
            if (await _context.Utenti.AnyAsync(u=> u.UserName.ToLower() == username.ToLower()))
            {
                return true;
            }
            return false;
        }

        //generazione di Salt e Hash utilizando HMACSHA512
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
