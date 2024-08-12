namespace coIT.Database.Entities
{
    public class DebitoorDraftError : DebitoorError
    {
        public Uri Uri => new Uri($"https://app.debitoor.com/invoices/drafts/{DebitoorId}");
    }
}
