using System.Collections.Immutable;
using coIT.Libraries.LexOffice.DataContracts.Invoice;
using CSharpFunctionalExtensions;

namespace coIT.Libraries.Lexoffice.BusinessRules.Rechnung.Leistungsempfänger
{
    internal class AlleLeistungsempfängerRegeln : IchPrüfe<Invoice>
    {
        private readonly LeistungsEmpfängerExistiert _leistungsempfängerExistiertRegel;
        private readonly LeistungsempfängerHatDebitornummer _leistungsempfängerHatDebitornummerRegel;

        public AlleLeistungsempfängerRegeln(
            IImmutableList<(string Id, int DebitorNummer)> leistungsempfängerMitDebitornummer
        )
        {
            var bekannteLeistungsempfängerIds = leistungsempfängerMitDebitornummer
                .Select(l => l.Id)
                .ToImmutableHashSet();
            _leistungsempfängerExistiertRegel = new LeistungsEmpfängerExistiert(
                bekannteLeistungsempfängerIds
            );

            _leistungsempfängerHatDebitornummerRegel = new LeistungsempfängerHatDebitornummer(
                leistungsempfängerMitDebitornummer
            );
        }

        public Result Prüfen(Invoice rechnung)
        {
            return _leistungsempfängerExistiertRegel
                .Prüfen(rechnung)
                .Bind(() => _leistungsempfängerHatDebitornummerRegel.Prüfen(rechnung));
        }
    }
}
