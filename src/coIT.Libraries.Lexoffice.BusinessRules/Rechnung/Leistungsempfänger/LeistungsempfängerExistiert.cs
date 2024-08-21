using System.Collections.Immutable;
using coIT.Libraries.Datengrundlagen.Kunden;
using coIT.Libraries.LexOffice.DataContracts.Invoice;
using CSharpFunctionalExtensions;

namespace coIT.Libraries.Lexoffice.BusinessRules.Rechnung.Leistungsempfänger
{
    internal class LeistungsEmpfängerExistiert : IchPrüfe<Invoice>
    {
        private readonly IImmutableSet<string> _bekannteLeistungsempfänger;

        public LeistungsEmpfängerExistiert(IImmutableList<Kunde> leistungsempfängerMitDebitornummer)
        {
            _bekannteLeistungsempfänger = leistungsempfängerMitDebitornummer
                .Select(l => l.Id)
                .ToImmutableHashSet();
        }

        public Result Prüfen(Invoice rechnung)
        {
            var leistungsEmpfängerAufRechnungId = rechnung.Address.ContactId;

            var leistungsempfängerOderNull = _bekannteLeistungsempfänger.FirstOrDefault(
                bekannterLeistungsempfänger =>
                    bekannterLeistungsempfänger == rechnung.Address.ContactId
            );

            if (leistungsempfängerOderNull is not null)
                return Result.Success();

            return Result.Failure(
                $"Der Leistungsempfägner mit dem Namen '{rechnung.Address.Name}' und der ID  '{leistungsEmpfängerAufRechnungId}' konnte keinem Leistungsempfänger zugeordnet werden. Überprüfe ob du den richtigen Kunden angegeben hast. Kontaktiere Uli, falls dies ein neurer Kunde ist."
            );
        }
    }
}
