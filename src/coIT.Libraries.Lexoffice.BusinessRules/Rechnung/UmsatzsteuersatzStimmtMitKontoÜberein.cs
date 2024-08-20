using System.Collections.Immutable;
using coIT.Libraries.Datengrundlagen.Konten;
using coIT.Libraries.LexOffice;
using coIT.Libraries.LexOffice.DataContracts.Invoice;
using CSharpFunctionalExtensions;

namespace coIT.Libraries.Lexoffice.BusinessRules.Rechnung
{
    internal class UmsatzsteuersatzStimmtMitKontoÜberein : IchPrüfe<Invoice>
    {
        private readonly IImmutableList<KontoDetails> _konten;

        public UmsatzsteuersatzStimmtMitKontoÜberein(IImmutableList<KontoDetails> konten)
        {
            _konten = konten;
        }

        public Result Prüfen(Invoice rechnung)
        {
            return rechnung
                .LineItems.First()
                .KontoErmitteln()
                .Map(kontoNummer =>
                    _konten.FirstOrDefault(k => k.KontoNummer == kontoNummer)?.Steuerrate ?? 0
                )
                .Ensure(
                    steuerrate => rechnung.TaxAmounts.First().TaxRatePercentage == steuerrate,
                    steuerrate =>
                        $"Es wird eine Steuerrate von {steuerrate}% erwartet jedoch wurde {rechnung.TaxAmounts.First().TaxRatePercentage} gesetzt"
                );
        }
    }
}
