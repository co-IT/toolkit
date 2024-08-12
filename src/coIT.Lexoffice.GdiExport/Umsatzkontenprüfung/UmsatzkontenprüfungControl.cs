using System.Data;
using coIT.Libraries.Clockodo.Absences;
using coIT.Libraries.Gdi.Accounting;
using coIT.Libraries.Gdi.Accounting.Contracts;
using coIT.Libraries.LexOffice;
using coIT.Libraries.LexOffice.DataContracts.Contacts;
using CSharpFunctionalExtensions;
using LexOfficeInvoice = coIT.Libraries.LexOffice.DataContracts.Invoice.Invoice;

namespace coIT.Lexoffice.GdiExport.Umsatzkontenprüfung
{
    internal partial class UmsatzkontenprüfungControl : UserControl
    {
        private Cache _cache;
        private readonly LexofficeService _lexofficeService;

        internal UmsatzkontenprüfungControl(LexofficeService lexofficeService)
        {
            InitializeComponent();
            _lexofficeService = lexofficeService;
        }

        private void InitiiereStandardAbfragezeitraum()
        {
            //setze Controls
        }

        private (DateOnly Von, DateOnly Bis) Zeitraum()
        {
            //lade Controls

            var jetzt = DateTime.Now;
            var anfangDesJahres = new DateTime(jetzt.Year, 1, 1);

            return (anfangDesJahres.ToDateOnly(), jetzt.ToDateOnly());
        }

        private async void UmsatzkontenprüfungControl_Load(object sender, EventArgs e)
        {
            tvErgebnis.Font = new Font(FontFamily.GenericMonospace, tvErgebnis.Font.Size);
            btnCsvAuswählen.Enabled = false;

            InitiiereStandardAbfragezeitraum();

            //_cache = new Cache(_lexofficeService);
            _cache = await Cache.LadeCacheAusLokalerDatei();

            //Lokale Cache-Datei neu erzeugen
            //var rechnungen = await _cache.Rechnungen(Zeitraum());
            //var kunden = await _cache.Kunden();
            //await Cache.ErzeugeLokalenCache(rechnungen, kunden);

            await UmsatzAbfragenKlick();
        }

        private async Task UmsatzAbfragenKlick()
        {
            var umsätze = await UmsätzeLaden(Zeitraum());
            UmsätzeAnzeigen(umsätze);
            btnCsvAuswählen.Enabled = true;
        }

        private async Task<List<VersendeteRechnung>> UmsätzeLaden(
            (DateOnly Von, DateOnly Bis) zeitraum
        )
        {
            var rechnungen = await _cache.Rechnungen(zeitraum);
            var kunden = await _cache.Kunden();

            var umsätze = UmsatzlisteErstellen(kunden, rechnungen);

            return umsätze;
        }

        private void UmsätzeAnzeigen(IEnumerable<VersendeteRechnung> umsätze)
        {
            var nachKontoGruppierteZeilen = umsätze
                .GroupBy(zeile => zeile.Umsatzkonto)
                .OrderBy(gruppe => gruppe.Key)
                .Select(gruppe => new
                {
                    Umsatzkonto = gruppe.Key,
                    Rechnungen = gruppe.OrderBy(r => r.Datum)
                });

            tvErgebnis.BeginUpdate();

            tvErgebnis.Nodes.Clear();

            var derzeitigesKonto = 0;
            foreach (var kontoMitRechnugen in nachKontoGruppierteZeilen)
            {
                var kontoSummeNetto = kontoMitRechnugen.Rechnungen.Sum(rechnung => rechnung.Netto);
                var kontoSummeBrutto = kontoMitRechnugen.Rechnungen.Sum(rechnung =>
                    rechnung.Brutto
                );

                var kontoText =
                    $"{kontoMitRechnugen.Umsatzkonto}: {kontoSummeNetto:C2} | {kontoSummeBrutto:C2}";

                tvErgebnis.Nodes.Add(kontoText);

                foreach (var rechnung in kontoMitRechnugen.Rechnungen)
                {
                    tvErgebnis.Nodes[derzeitigesKonto].Nodes.Add(rechnung.ToString());
                }

                derzeitigesKonto++;
            }

            tvErgebnis.EndUpdate();
        }

        private static int LängsterKundenname(List<ContactInformation> kunden) =>
            kunden.Max(kunde => kunde?.Company?.Name?.Length ?? 0);

        private static List<VersendeteRechnung> UmsatzlisteErstellen(
            List<ContactInformation> kunden,
            List<LexOfficeInvoice> rechnungen
        )
        {
            var längsterKundenName = LängsterKundenname(kunden);
            return rechnungen
                .Select(rechnung =>
                {
                    var kundenname =
                        kunden
                            .SingleOrDefault(kunde => kunde.Id == rechnung.Address.ContactId)
                            ?.Company?.Name ?? string.Empty;

                    return new VersendeteRechnung
                    {
                        Umsatzkonto = rechnung.KontoErmitteln(),
                        Datum = DateTime.Parse(rechnung.VoucherDate).ToDateOnly(),
                        Kundenname = kundenname,
                        KundennameLänge = längsterKundenName,
                        Nummer = rechnung.VoucherNumber,
                        Netto = rechnung.TotalPrice.TotalNetAmount,
                        Brutto = rechnung.TotalPrice.TotalGrossAmount
                    };
                })
                .ToList();
        }

        private async void btnCsvAuswählen_Click(object sender, EventArgs e)
        {
            if (dlgCsvÖffnen.ShowDialog() != DialogResult.OK)
                return;

            var dateiPfad = dlgCsvÖffnen.FileName;
            tbxCsvPfad.Text = Path.GetFileName(dateiPfad);

            var buchungenEinlesenErgebnis = await UmsatzParser.ParseCsv(dateiPfad);

            if (buchungenEinlesenErgebnis.IsFailure)
            {
                MessageBox.Show(
                    "Fehler beim Einlesen der csv Datei",
                    "Fehlgeschlagen",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return;
            }

            btnCsvAuswählen.Enabled = false;

            var buchungen = buchungenEinlesenErgebnis.Value.ToList();

            buchungen = DienstfahrzeugUmsätzeFiltern(buchungen);

            var gestellteRechnungen = await UmsätzeLaden(Zeitraum());
            var abgleichung = AbgleichBuchhaltung.FindeAbweichungenZuRechnungen(
                buchungen,
                gestellteRechnungen
            );
            AbweichungenAnzeigen(abgleichung);

            btnCsvAuswählen.Enabled = true;
        }

        private List<SaleBooking> DienstfahrzeugUmsätzeFiltern(List<SaleBooking> buchungen)
        {
            return buchungen.Where(buchung => buchung.AccountNumber != 8611).ToList();
        }

        private void AbweichungenAnzeigen(IList<Abweichung> abweichungen)
        {
            var nachFehlerGruppierteAbweichungen = abweichungen
                .GroupBy(zeile => zeile.Ergebnis)
                .OrderBy(gruppe => gruppe.Key)
                .Select(gruppe => new { Ergebnis = gruppe.Key, Abweichungen = gruppe });

            tvErgebnis.BeginUpdate();

            tvErgebnis.Nodes.Clear();

            var derzeitigesErgebnis = 0;
            foreach (var abweichungenFürErgebnis in nachFehlerGruppierteAbweichungen)
            {
                tvErgebnis.Nodes.Add(abweichungenFürErgebnis.Ergebnis);

                foreach (var rechnung in abweichungenFürErgebnis.Abweichungen)
                {
                    tvErgebnis
                        .Nodes[derzeitigesErgebnis]
                        .Nodes.Add($"{rechnung.Rechnungsnr}: {rechnung.Fehler}");
                }

                derzeitigesErgebnis++;
            }

            tvErgebnis.EndUpdate();
        }
    }
}
