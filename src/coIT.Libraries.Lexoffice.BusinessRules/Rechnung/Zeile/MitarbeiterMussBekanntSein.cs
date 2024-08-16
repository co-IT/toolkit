using System.Collections.Immutable;
using coIT.Libraries.Datengrundlagen.Mitarbeiter;
using coIT.Libraries.LexOffice;
using coIT.Libraries.LexOffice.DataContracts.Invoice;
using CSharpFunctionalExtensions;

namespace coIT.Libraries.Lexoffice.BusinessRules.Rechnung.Zeile
{
    internal class MitarbeiterMussBekanntSein : IchPrüfe<InvoiceLineItem>
    {
        private readonly IImmutableList<Mitarbeiter> _mitarbeiter;

        public MitarbeiterMussBekanntSein(IImmutableList<Mitarbeiter> mitarbeiter)
        {
            _mitarbeiter = mitarbeiter;
        }

        public Result Prüfen(InvoiceLineItem rechnungsZeile)
        {
            if (rechnungsZeile.Type is "text")
                return Result.Success();

            return rechnungsZeile
                .MitarbeiterErmitteln()
                .Ensure(
                    mitarbeiterNummer =>
                        _mitarbeiter.Count(bekannterMitarbeiter =>
                            bekannterMitarbeiter.Nummer == mitarbeiterNummer
                        ) == 1,
                    mitarbeiterNummer =>
                        $"Die Mitarbeiternummer {mitarbeiterNummer} ist nicht bekannt"
                );
        }
    }
}
