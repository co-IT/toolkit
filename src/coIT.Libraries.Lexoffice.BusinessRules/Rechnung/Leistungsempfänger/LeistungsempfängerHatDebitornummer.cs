using System.Collections.Immutable;
using coIT.Libraries.LexOffice.DataContracts.Invoice;
using CSharpFunctionalExtensions;

namespace coIT.Libraries.Lexoffice.BusinessRules.Rechnung.Leistungsempfänger
{
    internal class LeistungsempfängerHatDebitornummer : IchPrüfe<Invoice>
    {
        private readonly IImmutableList<(
            string Id,
            int DebitorNummer
        )> _leistungsempfängerMitDebitornummer;

        public LeistungsempfängerHatDebitornummer(
            IImmutableList<(string, int)> leistungsempfängerMitDebitornummer
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
                    .DebitorNummer != 0;

            return Result.SuccessIf(
                debitornummerFestgelegt,
                $"Die Debitorennummer des Leistungsempfängers {leistungsEmpfängerAufRechnung} ist nicht gepflegt"
            );
        }
    }
}
