
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

        public DateOnly DataIntervento { get; set; }

        public string MetodoIntervento { get; set; }

        public string IdCollegamento { get; set; }

        public string Descrizione { get; set; }

        public string TipoIntervento { get; set; }

        public string DurataIntervento { get; set; }

        public DateOnly dataCreazioneRapporto { get; set; }


        public Rapporto()
        {
            dataCreazioneRapporto = DateOnly.FromDateTime(DateTime.Now);
        }
    }
}
