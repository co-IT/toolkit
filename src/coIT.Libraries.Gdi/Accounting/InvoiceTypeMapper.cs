using coIT.Libraries.Gdi.Accounting.Contracts;

namespace coIT.Libraries.Gdi.Accounting;

internal static class InvoiceTypeMapper
{
    public static char GdiInvoiceTypeToken(InvoiceType invoiceType)
    {
        return invoiceType switch
        {
            InvoiceType.Cancellation => 'G',
            InvoiceType.CreditNote => 'R',
            InvoiceType.Invoice => 'R',
            _ => throw new ArgumentException()
        };
    }
}
