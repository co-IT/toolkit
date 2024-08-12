namespace coIT.Database.Entities
{
    public class DeletableInvoice : Entity
    {
        public DateTime MarkedAsDeletableSince { get; private set; }
        public Invoice InvoiceMarkedAsDeletable { get; private set; }

        public DeletableInvoice() { }

        private DeletableInvoice(Invoice invoiceToMarkAsDeletable, DateTime markedAsDeletableSince)
        {
            MarkedAsDeletableSince = markedAsDeletableSince;
            InvoiceMarkedAsDeletable =
                invoiceToMarkAsDeletable
                ?? throw new ArgumentNullException(nameof(invoiceToMarkAsDeletable));
        }

        public static DeletableInvoice FromInvoice(Invoice invoiceToMarkAsDeletable) =>
            new DeletableInvoice(invoiceToMarkAsDeletable, DateTime.Now);
    }
}
