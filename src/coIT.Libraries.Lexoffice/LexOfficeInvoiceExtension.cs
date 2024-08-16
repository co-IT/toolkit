using coIT.Libraries.LexOffice.DataContracts.Invoice;
using CSharpFunctionalExtensions;
using Newtonsoft.Json.Linq;

namespace coIT.Libraries.LexOffice
{
    public static class LexOfficeInvoiceExtension
    {
        public static Result<int> KontoErmitteln(this Invoice rechnung)
        {
            var rechnung3DDruckerVerkauf = "43c7adcd-6702-4dc9-8e20-d39b6654b22b"; //RE-2024-000372
            if (
                rechnung.Id.Equals(
                    rechnung3DDruckerVerkauf,
                    StringComparison.InvariantCultureIgnoreCase
                )
            )
                return 8820;

            return rechnung
                .LineItems.First(lineItem => lineItem.Type is not "text")
                .KontoErmitteln();
        }

        public static Result<(int Kontonummer, int Mitarbeiternummer)> KontoUndMitarbeiterErmitteln(
            this InvoiceLineItem rechnungsZeile
        )
        {
            var fehlerNachricht =
                $"Bitte passe die Rechnungszeile '{rechnungsZeile.Name}' an. Sie folgt nicht dem zulässigen Format für Rechnungszeilen.";

            return Result
                .Success(rechnungsZeile.Name.Split("-"))
                .Ensure(mitarbeiterKontoArray => mitarbeiterKontoArray.Length > 1, fehlerNachricht)
                .BindZip(mitarbeiterKontoArray =>
                    MitarbeiterAusZeilensplitErkennen(mitarbeiterKontoArray[1])
                )
                .BindZip(
                    (zeilensplit, mitarbeiternummer) =>
                        MitarbeiterAusZeilensplitErkennen(zeilensplit[0])
                )
                .Map(
                    ((string[] zeilenSplit, int mitarbeiternummer, int kontonummer) ergebnisse) =>
                        (ergebnisse.kontonummer, ergebnisse.mitarbeiternummer)
                );
        }

        private static Result<int> KontoAusZeilensplitErkennen(string kontoAlsText)
        {
            int kontoNummer;
            bool success = int.TryParse(kontoAlsText, out kontoNummer);

            var fehlerNachricht =
                $"Bitte passe die Rechnungszeile mit der Kontonumme '{kontoAlsText}' an, da diese Kontonummer keine gültige Zahl ist.";
            return Result.SuccessIf(success, kontoNummer, fehlerNachricht);
        }

        private static Result<int> MitarbeiterAusZeilensplitErkennen(
            string mitarbeiterUndZeilenbeschreibung
        )
        {
            var mitarbeiternummerText = mitarbeiterUndZeilenbeschreibung.Split(":")[0].Trim();

            int mitarbeiterNummer;
            bool success = int.TryParse(mitarbeiternummerText, out mitarbeiterNummer);

            var fehlerNachricht =
                $"Bitte passe den Rechnungszeilenabschnitt '{mitarbeiterUndZeilenbeschreibung}' an, damit er dem vorgegebenen Format entspricht";
            return Result.SuccessIf(success, mitarbeiterNummer, fehlerNachricht);
        }

        public static Result<int> KontoErmitteln(this InvoiceLineItem rechnungsZeile)
        {
            return KontoUndMitarbeiterErmitteln(rechnungsZeile)
                .Map(kontoUndMitarbeiter => kontoUndMitarbeiter.Kontonummer);
        }

        public static Result<int> MitarbeiterErmitteln(this InvoiceLineItem rechnungsZeile)
        {
            return KontoUndMitarbeiterErmitteln(rechnungsZeile)
                .Map(kontoUndMitarbeiter => kontoUndMitarbeiter.Mitarbeiternummer);
        }
    }
}
