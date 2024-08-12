namespace coIT.Libraries.Gdi.Accounting.Contracts
{
    public class SaleBooking
    {
        public int AccountNumber { get; init; }
        public string InvoiceNumber { get; init; }
        public DateOnly Date { get; init; }
        public int CustomerNumber { get; init; }
        public string CustomerName { get; init; }
        public decimal NetValue { get; init; }
    }
}
