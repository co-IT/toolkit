using System.Collections.Immutable;
using coIT.Libraries.Datengrundlagen.Kunden;
using coIT.Libraries.LexOffice.DataContracts.Invoice;
using CSharpFunctionalExtensions;

namespace coIT.Libraries.Lexoffice.BusinessRules.Rechnung.Leistungsempfänger
{
    internal class LeistungsempfängerHatDebitornummer : IchPrüfe<Invoice>
    {
        private readonly IImmutableList<Kunde> _leistungsempfängerMitDebitornummer;

        public LeistungsempfängerHatDebitornummer(
            IImmutableList<Kunde> leistungsempfängerMitDebitornummer
        )
        {
            _leistungsempfängerMitDebitornummer = leistungsempfängerMitDebitornummer;
        }

        public Result Prüfen(Invoice rechnung)
        {
            var leistungsEmpfängerAufRechnung = rechnung.Address.ContactId;

            var debitornummerFestgelegt =
                _leistungsempfängerMitDebitornummer
                    .FirstOrDefault(bekannterLeistungsempfänger =>
                        bekannterLeistungsempfänger.Id == leistungsEmpfängerAufRechnung
                    )
                    ?.Debitorennummer != 0;

            return Result.SuccessIf(
                debitornummerFestgelegt,
                $"Die Debitorennummer des Leistungsempfängers {leistungsEmpfängerAufRechnung} ist nicht gepflegt"
            );
        }
    }
}
