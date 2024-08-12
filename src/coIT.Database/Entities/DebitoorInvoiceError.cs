namespace coIT.Database.Entities
{
    public class DebitoorInvoiceError : DebitoorError
    {
        public Uri Uri => new Uri($"https://app.debitoor.com/invoices/{DebitoorId}");
    }
}
