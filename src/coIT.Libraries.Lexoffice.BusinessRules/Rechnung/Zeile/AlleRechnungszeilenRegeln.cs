using System.Collections.Immutable;
using coIT.Libraries.LexOffice.DataContracts.Invoice;
using CSharpFunctionalExtensions;

namespace coIT.Libraries.Lexoffice.BusinessRules.Rechnung.Zeile
{
    internal class AlleRechnungszeilenRegeln : IchPrüfe<InvoiceLineItem>
    {
        private readonly KontoMussBekanntSein _kontoBekanntRegel;
        private readonly MitarbeiterMussBekanntSein _mitarbeiterBekanntRegel;

        public AlleRechnungszeilenRegeln(
            IImmutableSet<int> bekannteKontonummern,
            IImmutableSet<int> mitarbeiter
        )
        {
            _kontoBekanntRegel = new KontoMussBekanntSein(bekannteKontonummern);
            _mitarbeiterBekanntRegel = new MitarbeiterMussBekanntSein(mitarbeiter);
        }

        public Result Prüfen(InvoiceLineItem rechnung)
        {
            return Result.Combine(
                _kontoBekanntRegel.Prüfen(rechnung),
                _mitarbeiterBekanntRegel.Prüfen(rechnung)
            );
        }
    }
}
