using coIT.Libraries.LexOffice;
using coIT.Libraries.LexOffice.DataContracts.Contacts;
using LexOfficeInvoice = coIT.Libraries.LexOffice.DataContracts.Invoice.Invoice;

namespace coIT.Lexoffice.GdiExport.Umsatzkontenprüfung.LexofficeCaching
{
    internal class TagesbasierterCache : IchCacheLexofficeAbfragen
    {
        private static Dictionary<DateOnly, List<LexOfficeInvoice>> _rechnungenCache = new();
        private static List<ContactInformation> _kundenCache;
        private readonly LexofficeService _service;

        public TagesbasierterCache(LexofficeService service)
        {
            _service = service;
        }

        public async Task<List<ContactInformation>> KundenAbfragen(bool cacheAktualisieren)
        {
            if (_kundenCache == null)
                _kundenCache = (await _service!.GetContactsAsync()).ToList();

            return _kundenCache;
        }

        public async Task<List<LexOfficeInvoice>> RechnungenAbfragen(
            (DateOnly Von, DateOnly Bis) zeitraum,
            bool cacheAktualisieren
        )
        {
            var tageInZeitraum = TageInAbfrageZeitraumErmitteln(zeitraum);

            var alleRechnungen = new List<LexOfficeInvoice>();
            foreach (var tag in tageInZeitraum)
            {
                var rechnungenFürTag = await RechnungenFürTagAbfragen(tag, cacheAktualisieren);
                alleRechnungen.AddRange(rechnungenFürTag);
            }

            return alleRechnungen;
        }

        private IEnumerable<DateOnly> TageInAbfrageZeitraumErmitteln(
            (DateOnly Von, DateOnly Bis) zeitraum
        )
        {
            for (
                DateOnly derzeitigerTag = zeitraum.Von;
                derzeitigerTag <= zeitraum.Bis;
                derzeitigerTag = derzeitigerTag.AddDays(1)
            )
            {
                yield return derzeitigerTag;
            }
        }

        public async Task<List<LexOfficeInvoice>> RechnungenFürTagAbfragen(
            DateOnly tag,
            bool cacheAktualisieren
        )
        {
            if (!cacheAktualisieren && _rechnungenCache.ContainsKey(tag))
                return _rechnungenCache[tag];

            var rechnungenDesTages = await RechnungenVonLexofficeAbrufen(tag);
            _rechnungenCache[tag] = rechnungenDesTages;
            return rechnungenDesTages;
        }

        private async Task<List<LexOfficeInvoice>> RechnungenVonLexofficeAbrufen(DateOnly tag)
        {
            var rechnungenInZeitraum = await _service!.GetVouchersInPeriod(tag, tag);
            var rechnungenMitDetails = await _service!.GetInvoicesAsync(rechnungenInZeitraum);

            return rechnungenMitDetails.ToList();
        }
    }
}
