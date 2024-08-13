using System.Collections.Immutable;
using coIT.Lexoffice.GdiExport.Kundenstamm;
using coIT.Lexoffice.GdiExport.Mitarbeiterliste;
using coIT.Lexoffice.GdiExport.Umsatzkonten;
using coIT.Libraries.Gdi.Accounting.Contracts;
using coIT.Libraries.Lexoffice.BusinessRules.Rechnung;
using coIT.Libraries.LexOffice;
using CSharpFunctionalExtensions;
using GdiInvoice = coIT.Libraries.Gdi.Accounting.Contracts.Invoice;
using LexofficeInvoice = coIT.Libraries.LexOffice.DataContracts.Invoice.Invoice;

namespace coIT.Lexoffice.GdiExport;

internal class InvoiceMapper
{
    private readonly IEnumerable<Kunde> _customers;
    private readonly IEnumerable<KontoDetails> _accounts;
    private readonly AlleRechnungsregeln _rechnungsRegelen;

    internal InvoiceMapper(
        IEnumerable<Kunde> customers,
        IEnumerable<KontoDetails> accounts,
        IEnumerable<Mitarbeiter> mitarbeiter
    )
    {
        _customers = customers;
        _accounts = accounts;

        var leistungsempfängerMitDebitornummer = _customers
            .Select(leistungsempfänger =>
                (leistungsempfänger.Id, leistungsempfänger.Debitorennummer)
            )
            .ToImmutableList();

        var kontoNummern = _accounts.Select(a => a.KontoNummer).ToImmutableHashSet();

        var mitarbeiterNummern = mitarbeiter.Select(m => m.Nummer).ToImmutableHashSet();

        _rechnungsRegelen = new AlleRechnungsregeln(
            leistungsempfängerMitDebitornummer,
            kontoNummern,
            mitarbeiterNummern
        );
    }

    internal Result<GdiInvoice> ToGdiInvoice(LexofficeInvoice lexOfficeInvoice)
    {
        var ergebnis = _rechnungsRegelen.Prüfen(lexOfficeInvoice);

        if (ergebnis.IsFailure)
            return ergebnis.ConvertFailure<GdiInvoice>();

        var exportCustomer = _customers.SingleOrDefault(customer =>
            customer.Id == lexOfficeInvoice.Address.ContactId
        );

        var accountNumber = lexOfficeInvoice.KontoErmitteln();

        var accountDetails = _accounts.FirstOrDefault(account =>
            account.KontoNummer == accountNumber
        );

        return new GdiInvoice
        {
            Date = DateTimeOffset.Parse(lexOfficeInvoice.VoucherDate),
            Number = lexOfficeInvoice.VoucherNumber,
            Type = InvoiceType.Invoice,
            NetAmount = lexOfficeInvoice.TotalPrice.TotalNetAmount,
            GrossAmount = lexOfficeInvoice.TotalPrice.TotalGrossAmount,
            TaxAmount = lexOfficeInvoice.TotalPrice.TotalTaxAmount,
            DebitorNumber = exportCustomer?.Debitorennummer ?? -1,
            DebitorName = exportCustomer?.DebitorName ?? "Unbekannt",
            RemoteId = lexOfficeInvoice.Id,
            DataSource = "lexoffice",
            RevenueAccountNumber = accountNumber,
            BillingAccountNumber = accountDetails.Steuerschlüssel
        };
    }
}
