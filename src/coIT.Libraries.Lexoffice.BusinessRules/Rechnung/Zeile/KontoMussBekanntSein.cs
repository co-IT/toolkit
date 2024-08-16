using System.Collections.Immutable;
using coIT.Libraries.Datengrundlagen.Konten;
using coIT.Libraries.LexOffice;
using coIT.Libraries.LexOffice.DataContracts.Invoice;
using CSharpFunctionalExtensions;

namespace coIT.Libraries.Lexoffice.BusinessRules.Rechnung.Zeile
{
    internal class KontoMussBekanntSein : IchPrüfe<InvoiceLineItem>
    {
        private readonly IImmutableList<KontoDetails> _bekannteKonnten;

        public KontoMussBekanntSein(IImmutableList<KontoDetails> bekannteKontonummern)
        {
            _bekannteKonnten = bekannteKontonummern;
        }

        public Result Prüfen(InvoiceLineItem rechnungsZeile)
        {
            if (rechnungsZeile.Type is "text")
                return Result.Success();

            return rechnungsZeile
                .KontoErmitteln()
                .Ensure(
                    kontoDerZeile =>
                        _bekannteKonnten.Count(konto => konto.KontoNummer == kontoDerZeile) == 1,
                    kontoDerZeile => $"Das Konto {kontoDerZeile} ist unbekannt."
                );
        }
    }
}
