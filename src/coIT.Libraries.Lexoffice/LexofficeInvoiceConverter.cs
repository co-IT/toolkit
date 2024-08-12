using System.Collections.Immutable;
using coIT.Libraries.LexOffice.DataContracts.Invoice;

namespace coIT.Libraries.LexOffice;

public static class LexofficeInvoiceConverter
{
    public static IImmutableList<InvoiceItem> GetInvoiceItems(this IImmutableList<Invoice> invoices)
    {
        return invoices.SelectMany(ConvertInvoice).ToImmutableList();
    }

    private static IImmutableList<InvoiceItem> ConvertInvoice(Invoice invoice)
    {
        return invoice.LineItems.Select(line => ConvertInvoice(invoice, line)).ToImmutableList();
    }

    private static InvoiceItem ConvertInvoice(Invoice invoice, InvoiceLineItem item)
    {
        var id = invoice.Id;
        var customer = invoice.Address.Name;
        var date = DateTime.Parse(invoice.VoucherDate);

        var employeeAccountString = item.Name;
        var employeeAccountStringSplit = employeeAccountString.Split("-");
        var employeeInfo = employeeAccountStringSplit[1].Split(":");
        var employeeNumber = int.Parse(employeeInfo[0].Replace(" ", ""));
        var employeeName = employeeInfo[1];
        var trimmedEmployeeName = employeeName.Trim(' ');
        var employee = new Employee(trimmedEmployeeName, employeeNumber);

        var account = int.Parse(employeeAccountStringSplit[0].Replace(" ", ""));

        return new InvoiceItem(
            invoice,
            id,
            customer,
            date,
            employee,
            account,
            item.Description,
            item.Quantity,
            item.UnitPrice.NetAmount * item.Quantity,
            item.UnitPrice.GrossAmount * item.Quantity
        );
    }
}
