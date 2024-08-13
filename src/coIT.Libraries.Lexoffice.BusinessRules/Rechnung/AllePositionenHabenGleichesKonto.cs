using coIT.Libraries.LexOffice;
using coIT.Libraries.LexOffice.DataContracts.Invoice;
using CSharpFunctionalExtensions;

namespace coIT.Libraries.Lexoffice.BusinessRules.Rechnung
{
    internal class AllePositionenHabenGleichesKonto : IchPrüfe<Invoice>
    {
        public Result Prüfen(Invoice rechnung)
        {
            var konten = rechnung
                .LineItems.Where(zeile => zeile.Type is not "text")
                .Select(zeile => zeile.KontoErmitteln())
                .Distinct();

            var verschiedeneKontenVorhanden = konten.Count() > 1;

            return Result.FailureIf(
                verschiedeneKontenVorhanden,
                $"Die Rechnung {rechnung.VoucherNumber} enthält mehrere verschiedene Kontonummern: "
                    + $"{string.Join(",", konten)}"
            );
        }
    }
}
