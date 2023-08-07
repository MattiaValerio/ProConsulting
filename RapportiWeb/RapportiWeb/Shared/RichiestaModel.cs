
namespace RapportiWeb.Shared
{
    public class Richiesta
    {
        public int id { get; set; }

        public DateOnly Data { get; set; }

        public int Clienteid { get; set; }

        public string ResponsabileRic { get; set; }

        public string Descrizione { get; set; }

        public string FiguraProfessionale { get; set; }

        public string TipologiaIntervento { get; set; }

        public DateOnly DataIntervento { get; set; }

        public string DurataIntervento { get; set; }

        public bool Firma { get; set; }

        public bool visible { get; set; }


        public Richiesta()
        {
            Firma = false;
            visible = false;

            Data = DateOnly.FromDateTime(DateTime.Now);
        }




    }
}
