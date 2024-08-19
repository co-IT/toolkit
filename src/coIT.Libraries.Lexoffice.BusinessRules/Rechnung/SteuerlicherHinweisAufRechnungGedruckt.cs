using System.Collections.Immutable;
using System.Globalization;
using coIT.Libraries.Datengrundlagen.Konten;
using coIT.Libraries.LexOffice;
using coIT.Libraries.LexOffice.DataContracts.Invoice;
using CSharpFunctionalExtensions;

namespace coIT.Libraries.Lexoffice.BusinessRules.Rechnung
{
    internal class SteuerlicherHinweisAufRechnungGedruckt : IchPrüfe<Invoice>
    {
        private readonly IImmutableList<KontoDetails> _konten;

        public SteuerlicherHinweisAufRechnungGedruckt(IImmutableList<KontoDetails> konten)
        {
            _konten = konten;
        }

        public Result Prüfen(Invoice rechnung)
        {
            return rechnung
                .LineItems.First()
                .KontoErmitteln()
                .Map(kontoNummer =>
                    _konten.FirstOrDefault(k => k.KontoNummer == kontoNummer)?.SteuerlicherHinweis
                    ?? string.Empty
                )
                .Ensure(
                    steuerlicherHinweis =>
                        CultureInfo.InvariantCulture.CompareInfo.IndexOf(
                            rechnung.PaymentConditions.PaymentTermLabel,
                            steuerlicherHinweis,
                            CompareOptions.IgnoreCase
                        ) >= 0,
                    steuerlicherHinweis =>
                        $"Aktualisiere die Rechnung mit dem korrekten angedruckten Text. Er muss '{steuerlicherHinweis}' enthalten."
                );
        }
    }
}
