using System.Collections.Immutable;
using System.Drawing.Text;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using coIT.Clockodo.QuickActions.Einstellungen;
using coIT.Libraries.Clockodo.TimeEntries;
using coIT.Libraries.Clockodo.TimeEntries.Contracts;
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
            return await LeistungsempfängerLaden()
                .BindZip((_) => BekannteKontenLaden())
                .BindZip((_, _) => AlleMitarbeiterLaden())
                .Map((tuple) => new AlleRechnungsregeln(tuple.First, tuple.Second, tuple.Third));
        }

        private async Task<Result<ImmutableList<Mitarbeiter>>> AlleMitarbeiterLaden()
        {
            return await MitarbeiterAusDateisystemLaden()
                .BindZip((_) => MitarbeiterAusClockodoLaden())
                .Map(
                    (tuple) =>
                        tuple.First.ClockodoMitarbeiterHinzufügen(tuple.Second).ToImmutableList()
                );
        }

        private async Task<Result<List<UserWithTeam>>> MitarbeiterAusClockodoLaden()
        {
            return await _environmentManager
                .Get<ClockodoEinstellungen>()
                .Map((konfiguration) => new TimeEntriesService(konfiguration.ClockodoCredentials))
                .Map((clockodoService) => clockodoService.GetAllUsers())
                .Map((mitarbeiter) => mitarbeiter.ToList());
        }

        private async Task<Result<MitarbeiterListe>> MitarbeiterAusDateisystemLaden()
        {
            return await _fileSystemManager.Get<MitarbeiterListe>();
        }

        private async Task<Result<ImmutableList<KontoDetails>>> BekannteKontenLaden()
        {
            return await _fileSystemManager
                .Get<UmsatzkontenListe>()
                .Map(kontenliste => kontenliste.ToImmutableList());
        }

        private async Task<Result<ImmutableList<Kunde>>> LeistungsempfängerLaden()
        {
            return (await _fileSystemManager.Get<Kundenstamm>()).Map(kundenstamm =>
                kundenstamm.ToImmutableList()
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
