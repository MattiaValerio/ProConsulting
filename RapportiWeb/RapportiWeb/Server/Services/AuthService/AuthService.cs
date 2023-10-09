using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RapportiWeb.Server.Data;
using RapportiWeb.Shared;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace RapportiWeb.Server.Services.AuthService
{
    public class AuthService : IAuthService
    {

        public DataContext _context { get; }
        public IConfiguration _conf { get; }

        public AuthService(DataContext context, IConfiguration conf)
        {
            _context = context;
            _conf = conf;
        }

        //Esegue la registrazione di un utente
        //Verifica che l'username non sia gia presente in DB e poi genera Hash e Salt per la password
        //Aggiunge l'user al DB e ritorna un ServiceResponse con l'id del nuovo utente
        public async Task<ServiceResponse<int>> Register(User user, string password)
        {
            try
            {
                if (await UserExists(user.UserName))
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

                _context.Add(user);
                await _context.SaveChangesAsync();

                return new ServiceResponse<int>
                {
                    Success = true,
                    Data = user.Id,
                    Message = "Utente creato con successo"
                };
            }
            catch
            {
                return new ServiceResponse<int>
                {
                    Success = false,
                    Message = "Errore durante la registrazione"
                };
            }
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

        public async Task<ServiceResponse<string>> Login(string username, string password)
        {
            var response = new ServiceResponse<string>();
            var user = await _context.Utenti.FirstOrDefaultAsync(u=> u.UserName.ToLower() == username.ToLower());

            if (user == null)
            {
                response.Data = string.Empty;
                response.Message = "Utente non trovato";
                response.Success = false;
            }

            if (user != null)
            {
                if (!VerifiyPasswordHash(password, user.Hash, user.Salt))
                {
                    response.Success = false;
                    response.Data = string.Empty;
                    response.Message = "Password errata.";
                }
                else
                {
                    response.Success = true;
                    response.Message = "Login eseguito con successo.";
                    response.Data = CreateToke(user);
                } 
            }

            return response;
        }

        private bool VerifiyPasswordHash(string password, byte[] hash, byte[] salt)
        {
            using (var hmac = new HMACSHA512(salt))
            {
                var computedhash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedhash.SequenceEqual(hash);
            }
        }



        private string CreateToke(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),//id dell'utente
                new Claim(ClaimTypes.Name, user.Nome), //nome
                new Claim(ClaimTypes.Surname, user.Cognome), //cognome
                new Claim(ClaimTypes.GivenName, user.UserName), //username
                new Claim(ClaimTypes.IsPersistent, user.Amministratore.ToString()), //amministratore
                new Claim(ClaimTypes.Role, user.TipoUtente), //tipo utente -> tecnico/cliente
                new Claim(ClaimTypes.Rsa, user.Figuraprof), //figura
                new Claim(ClaimTypes.System, user.ClienteId.ToString())
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                .GetBytes(_conf.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        } 
    }
}
