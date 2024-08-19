using System.Collections.Immutable;
using coIT.Libraries.Datengrundlagen.Kunden;
using coIT.Libraries.LexOffice.DataContracts.Invoice;
using CSharpFunctionalExtensions;

namespace coIT.Libraries.Lexoffice.BusinessRules.Rechnung.Leistungsempfänger
{
    internal class AlleLeistungsempfängerRegeln : IchPrüfe<Invoice>
    {
        private readonly LeistungsEmpfängerExistiert _leistungsempfängerExistiertRegel;
        private readonly LeistungsempfängerHatDebitornummer _leistungsempfängerHatDebitornummerRegel;
        private readonly LeistungsempfängerNutztKorrektesKonto _leistungsempfängerKontoRegel;

        public AlleLeistungsempfängerRegeln(
            IImmutableList<Kunde> leistungsempfängerMitDebitornummer
        )
        {
            _leistungsempfängerExistiertRegel = new LeistungsEmpfängerExistiert(
                leistungsempfängerMitDebitornummer
            );

            _leistungsempfängerHatDebitornummerRegel = new LeistungsempfängerHatDebitornummer(
                leistungsempfängerMitDebitornummer
            );

            _leistungsempfängerKontoRegel = new LeistungsempfängerNutztKorrektesKonto(
                leistungsempfängerMitDebitornummer
            );
        }

        public Result Prüfen(Invoice rechnung)
        {
            return _leistungsempfängerExistiertRegel
                .Prüfen(rechnung)
                .Bind(() => _leistungsempfängerHatDebitornummerRegel.Prüfen(rechnung))
                .Bind(() => _leistungsempfängerKontoRegel.Prüfen(rechnung));
        }
    }
}
