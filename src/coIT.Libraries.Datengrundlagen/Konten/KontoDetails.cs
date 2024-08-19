using System.ComponentModel;

namespace coIT.Libraries.Datengrundlagen.Konten
{
    public class KontoDetails
    {
        [Browsable(false)]
        public int Id { get; set; }
        public string KontoName { get; set; }
        public int KontoNummer { get; set; }
        public int KalkulatorischesKonto { get; set; }
        public string Geschäftssparte { get; set; }
        public bool IstBeratung { get; set; }
        public bool IstAbrechenbar { get; set; }
        public string SteuerlicherHinweis { get; set; }
        public int Steuerschlüssel { get; set; }
        public decimal Steuerrate { get; set; }

        public override string ToString()
        {
            return $"{KontoNummer} - {KontoName}";
        }
    }
}
