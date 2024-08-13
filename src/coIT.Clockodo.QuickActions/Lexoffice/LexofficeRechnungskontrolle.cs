using System.Collections.Immutable;
using System.Text.RegularExpressions;
using coIT.Libraries.Clockodo.TimeEntries;
using coIT.Libraries.LexOffice;
using coIT.Libraries.Lexoffice.BusinessRules;
using coIT.Libraries.Lexoffice.BusinessRules.Rechnung;
using coIT.Libraries.LexOffice.DataContracts.Invoice;
using CSharpFunctionalExtensions;

namespace coIT.Clockodo.QuickActions.Lexoffice
{
    public partial class LexofficeRechnungskontrolle : UserControl
    {
        //private readonly LexofficeService _lexofficeService;
        //private readonly TimeEntriesService _clockodoService;

        //public LexofficeRechnungskontrolle(
        //    LexofficeService lexofficeService,
        //    TimeEntriesService clockodoService
        //)
        //{
        //    InitializeComponent();
        //    _lexofficeService = lexofficeService;
        //    _clockodoService = clockodoService;
        //}

        private async void btnRechnungPrüfen_Click(object sender, EventArgs e)
        {
            var eingegebeneUrl = tbxRechnungUrl.Text;

            var test = await RechnungsIdAuslesen(eingegebeneUrl)
                .Map(RechnungsdatenVonLexofficeAbfragen)
                .Bind(RechnungÜberprüfen);
        }

        private Invoice RechnungsdatenVonLexofficeAbfragen(string rechnungsId)
        {
            return null;
        }

        private async Task<Result> RechnungÜberprüfen(Invoice rechnung)
        {
            return Result.Failure("Noch nicht implementiert");
        }

        //private async Task<IchPrüfe<Invoice>> RechnungsregelnErstellen()
        //{
        //    var kunden = await _lexofficeService.GetContactsAsync();
        //    var mitarbeiter = await _clockodoService.GetAllUsers();
        //    //var kontenListe = ???;

        //    var mitarbeiterNummern = mitarbeiter
        //        .Select(m => int.Parse(m.Number))
        //        .ToImmutableHashSet();
        //}

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
