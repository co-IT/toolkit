using System.Collections.Immutable;
using coIT.Libraries.Datengrundlagen.Konten;
using coIT.Libraries.Datengrundlagen.Kunden;
using coIT.Libraries.Datengrundlagen.Mitarbeiter;
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
        private readonly RechnungsnummerIstGültig _rechnungsnummerIstGültig;
        private readonly SteuerlicherHinweisAufRechnungGedruckt _rechnungEnhältKorrektenHinweis;
        private readonly AlleLeistungsempfängerRegeln _leistungsempfängerIstKorret;
        private readonly UmsatzsteuersatzStimmtMitKontoÜberein _steuerrateIstKorrekt;

        public AlleRechnungsregeln(
            IImmutableList<Kunde> leistungsempfängerMitDebitornummer,
            IImmutableList<KontoDetails> konten,
            IImmutableList<Mitarbeiter> mitarbeiter
        )
        {
            _rechungszeileRegeln = new AlleRechnungszeilenRegeln(konten, mitarbeiter);
            _allePositionenHabenGleichesKonto = new AllePositionenHabenGleichesKonto();
            _rechnungsnummerIstGültig = new RechnungsnummerIstGültig();
            _rechnungEnhältKorrektenHinweis = new SteuerlicherHinweisAufRechnungGedruckt(konten);
            _leistungsempfängerIstKorret = new AlleLeistungsempfängerRegeln(
                leistungsempfängerMitDebitornummer
            );
            _steuerrateIstKorrekt = new UmsatzsteuersatzStimmtMitKontoÜberein(konten);
        }

        public Result Prüfen(Invoice rechnung)
        {
            var leistungsemfängerPrüfungErgebnis = _leistungsempfängerIstKorret.Prüfen(rechnung);
            var rechnungsPrüfungenDieAufKontoBasieren = _allePositionenHabenGleichesKonto
                .Prüfen(rechnung)
                .Bind(
                    () =>
                        Result.Combine(
                            _rechnungEnhältKorrektenHinweis.Prüfen(rechnung),
                            _steuerrateIstKorrekt.Prüfen(rechnung)
                        )
                );

            var rechnungsnummerGültigErgebnis = _rechnungsnummerIstGültig.Prüfen(rechnung);

            return Result.Combine(
                ZeilenPrüfen(rechnung),
                leistungsemfängerPrüfungErgebnis,
                rechnungsPrüfungenDieAufKontoBasieren,
                rechnungsnummerGültigErgebnis
            );
        }

        public Result ZeilenPrüfen(Invoice rechnung)
        {
            var prüfungsErgebnisse = rechnung.LineItems.Select(_rechungszeileRegeln.Prüfen);

            return Result.Combine(prüfungsErgebnisse);
        }
    }
}
