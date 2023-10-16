
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RapportiWeb.Shared
{
    public class Richiesta
    {
        public int id { get; set; }
        //public string CustomCodice { get; set; } //codice univoco di 10 caratteri creabile dall'utente
        public DateTime Data { get; set; }
        public int Clienteid { get; set; }
		[Required(ErrorMessage = "Campo obbligatorio")]
		public string ResponsabileRic { get; set; }
        [Required(ErrorMessage = "Campo obbligatorio")]
        public string Descrizione { get; set; }
		[Required(ErrorMessage = "Campo obbligatorio")]
		public string FiguraProfessionale { get; set; }
		[Required(ErrorMessage = "Campo obbligatorio")]
		public string TipologiaIntervento { get; set; }
        [Required(ErrorMessage="Campo obbligatorio!")]
        public DateTime? DataIntervento { get; set; }
		[Required(ErrorMessage = "Campo obbligatorio")]
		public string DurataIntervento { get; set; }
        public bool Firma { get; set; }
        public bool visible { get; set; }
        public bool RapportoCreato { get; set; }
        //public string Creator { get; set; }


        public Richiesta()
        {
            Firma = false;
            visible = false;
            Data = DateTime.Now;
        }

    }
}
