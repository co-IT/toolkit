using System.Collections.Immutable;
using coIT.Libraries.Datengrundlagen.Kunden;
using coIT.Libraries.LexOffice;
using coIT.Libraries.LexOffice.DataContracts.Invoice;
using CSharpFunctionalExtensions;

namespace coIT.Libraries.Lexoffice.BusinessRules.Rechnung
{
    internal class LeistungsempfängerNutztKorrektesKonto : IchPrüfe<Invoice>
    {
        private readonly IImmutableList<Kunde> _leistungsempfänger;

        public LeistungsempfängerNutztKorrektesKonto(IImmutableList<Kunde> leistungsempfänger)
        {
            _leistungsempfänger = leistungsempfänger;
        }

        public Result Prüfen(Invoice rechnung)
        {
            var leistungsEmpfängerAufRechnung = rechnung.Address.ContactId;

            var leistungsEmpfängerFestgelegt = _leistungsempfänger.FirstOrDefault(
                bekannterLeistungsempfänger =>
                    bekannterLeistungsempfänger.Id == leistungsEmpfängerAufRechnung
            );

            var debitorNummer = leistungsEmpfängerFestgelegt?.Debitorennummer ?? 0;

            var erlaubteKonten = Kontoregeln.ErlaubteKontenFürKunden(debitorNummer);
            return rechnung
                .LineItems.First()
                .KontoErmitteln()
                .Ensure(
                    erlaubteKonten.Contains,
                    $"Für diesen Leistungsempfänger müssen die Konten {string.Join(',', erlaubteKonten)} genutzt werden."
                );
        }
    }
}
