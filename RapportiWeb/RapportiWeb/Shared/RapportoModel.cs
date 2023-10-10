
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RapportiWeb.Shared
{
    public class Rapporto : ICloneable
    {
        public int id { get; set; }
        public int? RichiestaId { get; set; }
        public int Clienteid { get; set; }
		[Required(ErrorMessage = "Campo obbligatorio")]
		public string RespRic { get; set; }
		[Required(ErrorMessage = "Campo obbligatorio")]
		public string RespInt { get; set; }
		[Required(ErrorMessage = "Campo obbligatorio")]
		public string Incaricato { get; set; }
		[Required(ErrorMessage = "Campo obbligatorio")]
		public DateTime? DataIntervento { get; set; }
		[Required(ErrorMessage = "Campo obbligatorio")]
		public string LuogoIntervento { get; set; }
        public string? IdCollegamento { get; set; }
        public string Descrizione { get; set; }
		[Required(ErrorMessage = "Campo obbligatorio")]
		public string TipoIntervento { get; set; }
		[Required(ErrorMessage = "Campo obbligatorio")]
		public string DurataIntervento { get; set; }
        public DateTime dataCreazioneRapporto { get; set; }
        //public string Creator { get; set; }


        public Rapporto()
        {
            dataCreazioneRapporto = DateTime.Now;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
