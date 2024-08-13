using System.Collections.Immutable;
using coIT.Libraries.Lexoffice.BusinessRules.Rechnung.Leistungsempfänger;
using coIT.Libraries.Lexoffice.BusinessRules.Rechnung.Zeile;
using coIT.Libraries.LexOffice.DataContracts.Invoice;
using CSharpFunctionalExtensions;

namespace coIT.Libraries.Lexoffice.BusinessRules.Rechnung
{
    public class AlleRechnungsregeln : IchPrüfe<Invoice>
    {
        private readonly AlleRechnungszeilenRegeln _rechungszeileRegeln;
        private readonly AllePositionenHabenGleichesKonto _allePositionenHabenGleichesKonto;
        private readonly AlleLeistungsempfängerRegeln _leistungsempfängerIstKorret;

        public AlleRechnungsregeln(
            IImmutableList<(
                string LeistungsempfängerId,
                int DebitorNummer
            )> leistungsempfängerMitDebitornummer,
            IImmutableSet<int> bekannteKontonummern,
            IImmutableSet<int> mitarbeiter
        )
        {
            _rechungszeileRegeln = new AlleRechnungszeilenRegeln(bekannteKontonummern, mitarbeiter);
            _allePositionenHabenGleichesKonto = new AllePositionenHabenGleichesKonto();
            _leistungsempfängerIstKorret = new AlleLeistungsempfängerRegeln(
                leistungsempfängerMitDebitornummer
            );
        }

        public Result Prüfen(Invoice rechnung)
        {
            var leistungsemfängerPrüfungErgebnis = _leistungsempfängerIstKorret.Prüfen(rechnung);
            var rechnungspositionenGleichesKontoErgebnis = _allePositionenHabenGleichesKonto.Prüfen(
                rechnung
            );

            return Result.Combine(
                ZeilenPrüfen(rechnung),
                leistungsemfängerPrüfungErgebnis,
                rechnungspositionenGleichesKontoErgebnis
            );
        }

        public Result ZeilenPrüfen(Invoice rechnung)
        {
            var prüfungsErgebnisse = rechnung.LineItems.Select(_rechungszeileRegeln.Prüfen);

            return Result.Combine(prüfungsErgebnisse);
        }
    }
}
