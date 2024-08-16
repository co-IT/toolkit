using coIT.Libraries.Datengrundlagen.Kunden;

namespace coIT.Lexoffice.GdiExport.Kundenstamm.Filter
{
    internal class DebitorNameFilter : IFilterKunde
    {
        public string Keyword { get; set; }

        public DebitorNameFilter(string keyword)
        {
            Keyword = keyword;
        }

        public IEnumerable<Kunde> KriteriumTrifftZu(IEnumerable<Kunde> kunden)
        {
            return kunden.Where(kunde =>
                kunde.DebitorName.Contains(Keyword, StringComparison.OrdinalIgnoreCase)
            );
        }
    }
}
