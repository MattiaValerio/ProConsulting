
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RapportiWeb.Shared
{
    public class User
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Cognome { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string NomeCompleto => Nome + " " + Cognome;
        public byte[] Hash { get; set; }
        public byte[] Salt { get; set; }
        public string Figuraprof { get; set; } = string.Empty;
        public int Amministratore { get; set; }
        public int Attivo { get; set; }
        public string TipoUtente { get; set; } = string.Empty; //0= tecnico 1=cliente
        public int ClienteId { get; set; }
    }
}
