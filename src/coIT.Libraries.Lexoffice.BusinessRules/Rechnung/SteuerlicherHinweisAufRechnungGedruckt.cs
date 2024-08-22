using System.Collections.Immutable;
using System.Globalization;
using System.Text.RegularExpressions;
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
                .Map(steuerlicherHinweis => new Regex(steuerlicherHinweis))
                .Ensure(
                    steuerlicherHinweis => HinweisTextIstGesetzt(rechnung, steuerlicherHinweis),
                    steuerlicherHinweis =>
                        $"Aktualisiere die Rechnung mit dem korrekten angedruckten Text. Er muss '{steuerlicherHinweis}' enthalten."
                );
        }

        private bool HinweisTextIstGesetzt(Invoice rechnung, Regex text)
        {
            return EnthältString(rechnung.PaymentConditions.PaymentTermLabel, text)
                || EnthältString(rechnung.Remark, text);
        }

        private bool EnthältString(string? heuhaufen, Regex regex)
        {
            if (heuhaufen is null)
                return false;

            var heuhaufenLower = heuhaufen.ToLowerInvariant();
            return regex.IsMatch(heuhaufenLower);
        }
    }
}
