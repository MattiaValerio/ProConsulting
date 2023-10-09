using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapportiWeb.Shared
{
    public class UserRegistration
    {
        public string Nome { get; set; } = string.Empty;
        public string Cognome { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Figuraprof { get; set; } = string.Empty;
        public int Amministratore { get; set; }
        public int Attivo { get; set; }
        public string TipoUtente { get; set; } = string.Empty;
        public int ClienteId { get; set; } 
    }
}
