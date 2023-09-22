
namespace RapportiWeb.Shared
{
    public class Rapporto
    {
        public int id { get; set; }

        public int? RichiestaId { get; set; }

        public int Clienteid { get; set; }

        public string RespRic { get; set; }

        public string RespInt { get; set; }

        public string Incaricato { get; set; }

        public DateTime? DataIntervento { get; set; }

        public string LuogoIntervento { get; set; }

        public string? IdCollegamento { get; set; }

        public string Descrizione { get; set; }

        public string TipoIntervento { get; set; }

        public string DurataIntervento { get; set; }

        public DateTime dataCreazioneRapporto { get; set; }


        public Rapporto()
        {
            dataCreazioneRapporto = DateTime.Now;
        }
    }
}
