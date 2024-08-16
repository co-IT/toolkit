using System.Text.RegularExpressions;
using coIT.Libraries.LexOffice.DataContracts.Invoice;
using CSharpFunctionalExtensions;

namespace coIT.Libraries.Lexoffice.BusinessRules.Rechnung
{
    internal class RechnungsnummerIstGültig : IchPrüfe<Invoice>
    {
        public Result Prüfen(Invoice rechnung)
        {
            var rechnungsNummer = rechnung.VoucherNumber;
            var valideRechnungsnummerPattern = new Regex("^RE-20[0-5][0-9]-[0-9]{6}$");
            var fehlerText =
                $"Bitte passe die Rechnungsnummer ({rechnung.VoucherNumber}) an. Sie muss dem Format 'RE-20##-######' entsprechen";

            return Result.SuccessIf(
                valideRechnungsnummerPattern.IsMatch(rechnungsNummer),
                fehlerText
            );
        }
    }
}
