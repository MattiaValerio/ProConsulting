
namespace RapportiWeb.Shared
{
    public class Richiesta
    {
        public int id { get; set; }
        //public string CustomCodice { get; set; } //codice univoco di 10 caratteri creabile dall'utente
        public DateTime Data { get; set; }
        public int Clienteid { get; set; }
        public string ResponsabileRic { get; set; }
        public string Descrizione { get; set; }
        public string FiguraProfessionale { get; set; }
        public string TipologiaIntervento { get; set; }
        public DateTime? DataIntervento { get; set; }
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
