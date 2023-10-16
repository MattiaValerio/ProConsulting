using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RapportiWeb.Shared
{
    public class Cliente
    {
        public int id { get; set; }

		[Required(ErrorMessage = "Campo obbligatorio")]
		public string ragioneSociale { get; set; }

		[Required(ErrorMessage = "Campo obbligatorio")]
		public string NomeAbbreviato { get; set; }

		[Required(ErrorMessage = "Campo obbligatorio")]
        public string Indirizzo { get; set; }
         
        [Required(ErrorMessage = "Campo obbligatorio")]
        public string Citta { get; set; }

        public string? Provincia { get; set; }

        [Required(ErrorMessage = "Campo obbligatorio")]
        public int CAP { get; set; }

        public string? Stato { get; set; }

        [Required(ErrorMessage = "Campo obbligatorio")]
        public string telefono { get; set; }

        [Required(ErrorMessage = "Campo obbligatorio")]
        public string email { get; set; }

        public string? Responsabile { get; set; }

        public List<Richiesta>? Richieste { get; set; }

        public List<Rapporto>? Rapporti { get; set; }

        public List<User>? Utenti { get; set; }

        public Cliente()
        {
            Utenti = new List<User>();
            Richieste = new List<Richiesta>();
            Rapporti = new List<Rapporto>();
        }

    }
}
