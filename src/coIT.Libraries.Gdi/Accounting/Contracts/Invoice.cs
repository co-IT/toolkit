namespace coIT.Libraries.Gdi.Accounting.Contracts;

public class Invoice
{
    public DateTimeOffset Date { get; set; }
    public string Number { get; set; }
    public InvoiceType Type { get; set; }
    public decimal NetAmount { get; set; }
    public decimal GrossAmount { get; set; }
    public decimal TaxAmount { get; set; }

    public int DebitorNumber { get; set; }
    public string DebitorName { get; set; }

    public string RemoteId { get; set; }
    public string DataSource { get; set; }

    public int RevenueAccountNumber { get; set; }

    public decimal BillingAccountNumber { get; set; }
}
