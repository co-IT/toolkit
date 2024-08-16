using System.Collections.Immutable;
using coIT.Libraries.LexOffice;
using coIT.Libraries.LexOffice.DataContracts.Invoice;
using CSharpFunctionalExtensions;

namespace coIT.Libraries.Lexoffice.BusinessRules.Rechnung.Zeile
{
    internal class KontoMussBekanntSein : IchPrüfe<InvoiceLineItem>
    {
        private readonly IImmutableSet<int> _bekannteKontonummern;

        public KontoMussBekanntSein(IImmutableSet<int> bekannteKontonummern)
        {
            _bekannteKontonummern = bekannteKontonummern;
        }

        public Result Prüfen(InvoiceLineItem rechnungsZeile)
        {
            if (rechnungsZeile.Type is "text")
                return Result.Success();

            return rechnungsZeile
                .KontoErmitteln()
                .Ensure(
                    konto => _bekannteKontonummern.Count(nummer => nummer == konto) == 1,
                    konto => $"Das Konto {konto} ist unbekannt."
                );
        }
    }
}
