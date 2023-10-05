using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapportiWeb.Shared
{
    public class UserRegistration
    {
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Figuraprof { get; set; }
        public int Amministratore { get; set; }
        public int Attivo { get; set; }
        public int TipoUtente { get; set; } //0= tecnico 1=cliente
        public Cliente Organizzazione { get; set; }
    }
}
