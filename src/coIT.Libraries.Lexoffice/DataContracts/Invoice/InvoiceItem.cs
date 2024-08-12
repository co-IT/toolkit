namespace coIT.Libraries.LexOffice.DataContracts.Invoice;

public record InvoiceItem
{
    internal InvoiceItem(
        Invoice invoice,
        string invoiceId,
        string customer,
        DateTime date,
        Employee employee,
        int account,
        string description,
        decimal amountHours,
        decimal priceNetto,
        decimal priceGross
    )
    {
        Invoice = invoice;
        InvoiceId = invoiceId;
        Customer = customer;
        Date = date;
        Employee = employee;
        Account = account;
        Description = description;
        AmountHours = amountHours;
        PriceGross = priceGross;
        PriceNetto = priceNetto;
    }

    public Invoice Invoice { get; }
    public string InvoiceId { get; }
    public string Customer { get; }
    public DateTime Date { get; }
    public Employee Employee { get; }
    public int Account { get; }
    public string Description { get; }
    public decimal AmountHours { get; }
    public decimal PriceNetto { get; }
    public decimal PriceGross { get; }
}
