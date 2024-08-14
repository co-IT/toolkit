using System.Collections.Immutable;
using System.Drawing.Text;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using coIT.Clockodo.QuickActions.Einstellungen.Konten;
using coIT.Clockodo.QuickActions.Einstellungen.Kunden;
using coIT.Clockodo.QuickActions.Einstellungen.Mitarbeiter;
using coIT.Libraries.Clockodo.TimeEntries;
using coIT.Libraries.ConfigurationManager;
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
        private readonly LexofficeService _lexofficeService;
        private readonly TimeEntriesService _clockodoService;
        private readonly FileSystemManager _fileSystemManager;

        public LexofficeRechnungskontrolle(
            LexofficeService lexofficeService,
            TimeEntriesService clockodoService,
            FileSystemManager fileSystemManager
        )
        {
            InitializeComponent();
            _lexofficeService = lexofficeService;
            _clockodoService = clockodoService;
            _fileSystemManager = fileSystemManager;
        }

        private async void btnRechnungPrüfen_Click(object sender, EventArgs e)
        {
            EingabeBlockieren(true);

            var rechnungPrüfungsErgebnis = await Result
                .Success(tbxRechnungUrl.Text)
                .Bind(RechnungsIdAuslesen)
                .Bind(RechnungsdatenVonLexofficeAbfragen)
                .BindZip(_ => RechnungsprüferErstellen())
                .Bind(tuple => RechnungÜberprüfen(tuple.First, tuple.Second));

            PrüfergebnisAnzeigen(rechnungPrüfungsErgebnis);

            EingabeBlockieren(false);
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
            try
            {
                var invoice = await _lexofficeService.GetInvoiceAsync(rechnungsId);

                return Result.SuccessIf(
                    invoice is not null,
                    invoice!,
                    "Beim Abrufen der Rechnung gab es einen Fehler. Bitte überprüfe die URL und versuche es erneut."
                );
            }
            catch (Exception e)
            {
                return Result.Failure<Invoice>(
                    $"Stell sicher, dass du den richtigen Rechnungslink kopiert hast. Die Rechnung mit der ID {rechnungsId} konnte nicht gefunden werden."
                );
            }
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
            var mitarbeiterAusDateisystemErgebnis = await MitarbeiterAusDateisystemLaden();
            if (mitarbeiterAusDateisystemErgebnis.IsFailure)
                return mitarbeiterAusDateisystemErgebnis.Map(_ =>
                    new HashSet<int>().ToImmutableHashSet()
                );

            List<int> mitarbeiterIdsInClockodo = await MitarbeiterAusClockodoLaden();

            return mitarbeiterAusDateisystemErgebnis
                .Value.Concat(mitarbeiterIdsInClockodo)
                .ToImmutableHashSet();
        }

        private async Task<List<int>> MitarbeiterAusClockodoLaden()
        {
            var adminMitarbeiterId = 350599;
            var mitarbeiterIdsInClockodo = (await _clockodoService.GetAllUsers())
                .Where(mitarbeiter => mitarbeiter.Id != adminMitarbeiterId)
                .Select(mitarbeiter => int.Parse(mitarbeiter.Number))
                .ToList();
            return mitarbeiterIdsInClockodo;
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
