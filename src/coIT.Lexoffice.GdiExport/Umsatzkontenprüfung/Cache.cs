using System.Data;
using System.Text;
using coIT.Libraries.Clockodo.Absences;
using coIT.Libraries.LexOffice;
using coIT.Libraries.LexOffice.DataContracts.Contacts;
using CSharpFunctionalExtensions;
using CSharpFunctionalExtensions.ValueTasks;
using Newtonsoft.Json;
using LexOfficeInvoice = coIT.Libraries.LexOffice.DataContracts.Invoice.Invoice;

namespace coIT.Lexoffice.GdiExport.Umsatzkontenprüfung
{
    internal class Cache
    {
        private static Dictionary<string, List<LexOfficeInvoice>> _rechnungenCache = new();
        private static List<ContactInformation>? _kundenCache;
        private static string _lokaleDateiCacheKey = "json_datei";
        private static bool _nutzeLokaleDatei = false;
        private static string _rechnungenCacheDatei = "cache_rechnungen.json";
        private static string _kundenCacheDatei = "cache_kunden.json";

        private readonly LexofficeService? _service;

        public Cache(LexofficeService service)
        {
            _service = service;
        }

        private Cache(List<LexOfficeInvoice> rechnungen, List<ContactInformation> kunden)
        {
            _kundenCache = kunden;
            _rechnungenCache = new Dictionary<string, List<LexOfficeInvoice>>
            {
                { _lokaleDateiCacheKey, rechnungen }
            };
            _nutzeLokaleDatei = true;
        }

        public static async Task<Cache> LadeCacheAusLokalerDatei()
        {
            var rechnungenCache = await File.ReadAllTextAsync(_rechnungenCacheDatei, Encoding.UTF8);
            var rechnungen =
                JsonConvert.DeserializeObject<List<LexOfficeInvoice>>(rechnungenCache)
                ?? throw new InvalidDataException(_rechnungenCacheDatei);

            var kundenCache = await File.ReadAllTextAsync(_kundenCacheDatei, Encoding.UTF8);
            var kunden =
                JsonConvert.DeserializeObject<List<ContactInformation>>(kundenCache)
                ?? throw new InvalidDataException(_kundenCacheDatei);

            return new Cache(rechnungen, kunden);
        }

        public static async Task ErzeugeLokalenCache(
            List<LexOfficeInvoice> rechnungen,
            List<ContactInformation> kunden
        )
        {
            var rechnungenCache = JsonConvert.SerializeObject(rechnungen);
            await File.WriteAllTextAsync(
                _rechnungenCacheDatei,
                rechnungenCache,
                Encoding.UTF8,
                CancellationToken.None
            );

            var kundenCache = JsonConvert.SerializeObject(kunden);
            await File.WriteAllTextAsync(
                _kundenCacheDatei,
                kundenCache,
                Encoding.UTF8,
                CancellationToken.None
            );
        }

        private string CacheKey((DateOnly Von, DateOnly Bis) zeitraum)
        {
            if (_nutzeLokaleDatei)
                return _lokaleDateiCacheKey;

            return $"{zeitraum.Von.ToShortDateString()}{zeitraum.Bis.ToShortDateString()}";
        }

        public async Task<List<ContactInformation>> Kunden()
        {
            if (_kundenCache == null)
                _kundenCache = (await _service!.GetContactsAsync()).ToList();

            return _kundenCache;
        }

        public async Task<List<LexOfficeInvoice>> Rechnungen((DateOnly Von, DateOnly Bis) zeitraum)
        {
            var cacheKey = CacheKey(zeitraum);

            if (_rechnungenCache.ContainsKey(cacheKey))
            {
                return _rechnungenCache[cacheKey];
            }

            var gefilterteRechnungen = await RechnungenVonLexofficeAbrufen(zeitraum);

            _rechnungenCache.Add(cacheKey, gefilterteRechnungen.ToList());
            return _rechnungenCache[cacheKey];
        }

        private async Task<List<LexOfficeInvoice>> RechnungenVonLexofficeAbrufen(
            (DateOnly Von, DateOnly Bis) zeitraum
        )
        {
            var rechnungenInZeitraum = await _service!.GetVouchersInPeriod(
                zeitraum.Von,
                zeitraum.Bis
            );
            var rechnungenMitDetails = await _service!.GetInvoicesAsync(rechnungenInZeitraum);

            //workaround => eigentlich sollten schon die Rechnungen aus dem richtigen Zeitraum vom Service kommen
            //ist anscheinend nicht so
            var gefilterteRechnungen = rechnungenMitDetails
                .Where(rechnung =>
                    DateTime.Parse(rechnung.VoucherDate).ToDateOnly() >= zeitraum.Von
                )
                .Where(rechnung =>
                    DateTime.Parse(rechnung.VoucherDate).ToDateOnly() <= zeitraum.Bis
                );

            return gefilterteRechnungen.ToList();
        }
    }
}
