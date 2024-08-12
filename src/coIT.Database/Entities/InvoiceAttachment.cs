namespace coIT.Database.Entities
{
    public class InvoiceAttachment : Entity
    {
        public Guid DocumentSoreId { get; set; } //ToDo private set after migration
        public Uri DocumentStoreLink { get; set; } //ToDo private set after migration
        public int InvoiceId { get; set; } //ToDo private set after migration
        public AttachmentTypes AttachmentType { get; set; } //ToDo private set after migration
    }

    public enum AttachmentTypes
    {
        DefaultInvoice,
        Unknown
    }
}
