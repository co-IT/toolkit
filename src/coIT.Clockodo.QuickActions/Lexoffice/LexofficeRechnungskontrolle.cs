using System.Collections.Immutable;
using System.Drawing.Text;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using coIT.Clockodo.QuickActions.Einstellungen;
using coIT.Libraries.Clockodo.TimeEntries;
using coIT.Libraries.ConfigurationManager;
using coIT.Libraries.Datengrundlagen.Konten;
using coIT.Libraries.Datengrundlagen.Kunden;
using coIT.Libraries.Datengrundlagen.Mitarbeiter;
using coIT.Libraries.LexOffice;
using coIT.Libraries.Lexoffice.BusinessRules;
using coIT.Libraries.Lexoffice.BusinessRules.Rechnung;
using coIT.Libraries.LexOffice.DataContracts.Invoice;
using CSharpFunctionalExtensions;
using CSharpFunctionalExtensions.ValueTasks;

namespace coIT.Clockodo.QuickActions.Lexoffice
{
    public partial class LexofficeRechnungskontrolle : UserControl
    {
        private readonly FileSystemManager _fileSystemManager;
        private readonly EnvironmentManager _environmentManager;

        public LexofficeRechnungskontrolle(
            FileSystemManager fileSystemManager,
            EnvironmentManager environmentManager
        )
        {
            InitializeComponent();
            _fileSystemManager = fileSystemManager;
            _environmentManager = environmentManager;
        }

        private async void btnRechnungPrüfen_Click(object sender, EventArgs e)
        {
            EingabeBlockieren(false);

            var rechnungPrüfungsErgebnis = await Result
                .Success(tbxRechnungUrl.Text)
                .Bind(RechnungsIdAuslesen)
                .Bind(RechnungsdatenVonLexofficeAbfragen)
                .BindZip(_ => RechnungsprüferErstellen())
                .Bind(tuple => RechnungÜberprüfen(tuple.First, tuple.Second));

            PrüfergebnisAnzeigen(rechnungPrüfungsErgebnis);

            EingabeBlockieren(true);
        }

        private void EingabeBlockieren(bool blockieren)
        {
            btnRechnungPrüfen.Enabled = blockieren;
            tbxRechnungUrl.Enabled = blockieren;
        }

        private void PrüfergebnisAnzeigen(Result rechnungPrüfungsErgebnis)
        {
            if (rechnungPrüfungsErgebnis.IsFailure)
            {
                lblErgebnisse.Text =
                    $"Die Prüfung ist wegen folgenden Fehlern fehlgeschlagen: {Environment.NewLine}    "
                    + rechnungPrüfungsErgebnis.Error.Replace(", ", $"{Environment.NewLine}    ");
                return;
            }

            MessageBox.Show(
                "Es konnten keine Fehler in dieser Rechnung gefunden werden",
                "Gratulation",
                MessageBoxButtons.OK,
                MessageBoxIcon.None
            );
            lblErgebnisse.Text = "Es wurden keine Fehler in dieser Rechnung gefunden";
        }

        private async Task<Result<Invoice>> RechnungsdatenVonLexofficeAbfragen(string rechnungsId)
        {
            return await _environmentManager
                .Get<LexofficeKonfiguration>()
                .Map((konfiguration) => new LexofficeService(konfiguration.LexofficeKey))
                .MapTry(
                    (lexofficeService) => lexofficeService.GetInvoiceAsync(rechnungsId),
                    (_) =>
                        $"Hast du den richtigen Rechnungslink kopiert?. Die Rechnung mit der ID {rechnungsId} konnte nicht gefunden werden."
                )
                .Ensure(
                    (invoice) => invoice is not null,
                    "Beim Abrufen der Rechnung gab es einen Fehler. Bitte überprüfe die URL und versuche es erneut."
                );
        }

        private async Task<Result> RechnungÜberprüfen(Invoice rechnung, IchPrüfe<Invoice> prüfer)
        {
            return prüfer.Prüfen(rechnung);
        }

        private async Task<Result<AlleRechnungsregeln>> RechnungsprüferErstellen()
        {
            return await LeistungsempfängerMitDebitornummerLaden()
                .BindZip((_) => BekannteKontonummernErhalten())
                .BindZip((_, _) => AlleMitarbeiterIdsErhalten())
                .Map((tuple) => new AlleRechnungsregeln(tuple.First, tuple.Second, tuple.Third));
        }

        private async Task<Result<ImmutableHashSet<int>>> AlleMitarbeiterIdsErhalten()
        {
            return await MitarbeiterAusDateisystemLaden()
                .BindZip((_) => MitarbeiterAusClockodoLaden())
                .Map((tuple) => tuple.First.Concat(tuple.Second).ToImmutableHashSet());
        }

        private async Task<Result<List<int>>> MitarbeiterAusClockodoLaden()
        {
            var adminMitarbeiterId = 350599;
            return await _environmentManager
                .Get<ClockodoEinstellungen>()
                .Map((konfiguration) => new TimeEntriesService(konfiguration.ClockodoCredentials))
                .Map((clockodoService) => clockodoService.GetAllUsers())
                .Map(
                    (clockodoNutzer) =>
                        clockodoNutzer
                            .Where(mitarbeiter => mitarbeiter.Id != adminMitarbeiterId)
                            .Select(mitarbeiter => mitarbeiter.Number)
                            .ToList()
                )
                .MapTry(
                    (nummernListe) =>
                        nummernListe.Select(nummer => int.Parse(nummer ?? "0")).ToList(),
                    (_) => "Mitarbeiternummer im ungültigen Format gefunden"
                );
        }

        private async Task<Result<HashSet<int>>> MitarbeiterAusDateisystemLaden()
        {
            var mitarbeiterListeErgebnis = await _fileSystemManager.Get<MitarbeiterListe>();

            if (mitarbeiterListeErgebnis.IsFailure)
                return mitarbeiterListeErgebnis.Map(_ => new HashSet<int>());

            return mitarbeiterListeErgebnis
                .Value.Select(mitarbeiter => mitarbeiter.Nummer)
                .ToHashSet();
        }

        private async Task<Result<ImmutableHashSet<int>>> BekannteKontonummernErhalten()
        {
            var umsatzkontenErgebnis = await _fileSystemManager.Get<UmsatzkontenListe>();
            return umsatzkontenErgebnis.Map(konten =>
                konten.Select(konto => konto.KontoNummer).ToImmutableHashSet()
            );
        }

        private async Task<
            Result<ImmutableList<(string LeistungsempfängerId, int DebitorNummer)>>
        > LeistungsempfängerMitDebitornummerLaden()
        {
            var kundenStammErgebnis = await _fileSystemManager.Get<Kundenstamm>();
            return kundenStammErgebnis.Map(kunden =>
                kunden
                    .Select(leistungsempfänger =>
                        (leistungsempfänger.Id, leistungsempfänger.Debitorennummer)
                    )
                    .ToImmutableList()
            );
        }

        private Result<string> RechnungsIdAuslesen(string text)
        {
            var valideIdRegex = new Regex(
                "[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}$"
            );

            var valideIdÜbereinstimmungen = valideIdRegex.Match(text);
            if (!valideIdÜbereinstimmungen.Success)
                return Result.Failure<string>(
                    "Stell sicher, dass du die richtige URL eingefügt hast. Die angegebene URL konnte nicht erkannt werden."
                );

            var übereinstimmungen = valideIdRegex.Match(text);
            if (übereinstimmungen.Captures.Count > 1)
                return Result.Failure<string>(
                    "Stell sicher, dass du die richtige URL eingefügt hast. In der angegebenen URL wurden mehrere IDs gefunden."
                );

            return übereinstimmungen.Value;
        }
    }
}
