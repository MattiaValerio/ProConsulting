
namespace RapportiWeb.Shared
{
    public class User
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string UserName { get; set; }
        public byte[] Hash { get; set; }
        public byte[] Salt { get; set; }
        public int Figuraprof { get; set; }
        public int Amministratore { get; set; }
        public int Attivo { get; set; }
        public int TipoUtente { get; set; } //0= tecnico 1=cliente
        public Cliente Organizzazione { get; set; }
    }
}
