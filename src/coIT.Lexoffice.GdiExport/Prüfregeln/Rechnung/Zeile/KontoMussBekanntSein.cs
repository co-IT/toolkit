using System.Collections.Immutable;
using coIT.Libraries.LexOffice.DataContracts.Invoice;
using CSharpFunctionalExtensions;

namespace coIT.Lexoffice.GdiExport.Prüfregeln.Rechnung.Zeile
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

            var konto = rechnungsZeile.KontoErmitteln();

            var kontoExistiert = _bekannteKontonummern.Count(nummer => nummer == konto) == 1;

            return Result.SuccessIf(
                kontoExistiert,
                rechnungsZeile,
                $"Das Konto {konto} ist unbekannt."
            );
        }
    }
}
