using coIT.Libraries.LexOffice.DataContracts.Invoice;

namespace coIT.Libraries.LexOffice
{
    public static class LexOfficeInvoiceExtension
    {
        public static int KontoErmitteln(this Invoice rechnung)
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

        public static int KontoErmitteln(this InvoiceLineItem rechnungsZeile)
        {
            var mitarbeiterNummerKontoZeile = rechnungsZeile.Name;
            var mitarbeiterKontoArray = mitarbeiterNummerKontoZeile.Split("-");
            return int.Parse(mitarbeiterKontoArray[0].Replace(" ", ""));
        }

        public static int MitarbeiterErmitteln(this InvoiceLineItem rechnungsZeile)
        {
            var mitarbeiterNummerKontoZeile = rechnungsZeile.Name;
            var mitarbeiterUndZeilenbeschreibung = mitarbeiterNummerKontoZeile.Split("-")[1];
            var mitarbeiterNameMitLeerzeichen = mitarbeiterUndZeilenbeschreibung.Split(":")[0];
            var mitarbeiterName = mitarbeiterNameMitLeerzeichen.Trim();
            return int.Parse(mitarbeiterName);
        }
    }
}
