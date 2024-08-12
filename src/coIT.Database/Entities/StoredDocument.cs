using coIT.Database.Entities.ValueObjects;

// ReSharper disable ArrangeMethodOrOperatorBody

namespace coIT.Database.Entities
{
    //todo: IsPrimaryDocument von Bool zu Enum

    public class StoredDocument : Entity
    {
        public Guid DocumentId { get; private set; }

        public InvoiceNumber InvoiceNumber { get; private set; }

        public bool IsPrimaryDocument { get; private set; }

        public string MimeType { get; private set; }

        public string Filename { get; private set; }
        public int DmsId { get; private set; }

        public void SavedToDocumentManagementSystem(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id));

            DmsId = id;
        }

        public StoredDocument(
            Guid documentId,
            InvoiceNumber invoiceNumber,
            bool isPrimaryDocument,
            string mimeType,
            string filename
        )
        {
            if (documentId == Guid.Empty)
                throw new ArgumentOutOfRangeException(
                    nameof(documentId),
                    "DocumentId may not be empty"
                );

            if (string.IsNullOrWhiteSpace(mimeType))
                throw new ArgumentException(nameof(mimeType));

            if (string.IsNullOrWhiteSpace(filename))
                throw new ArgumentException(nameof(filename));

            DocumentId = documentId;
            InvoiceNumber = invoiceNumber ?? throw new ArgumentNullException(nameof(invoiceNumber));
            IsPrimaryDocument = isPrimaryDocument;
            MimeType = mimeType;
            Filename = filename;
        }

        public StoredDocument() { }

        public static StoredDocument CreatePrimaryDocument(
            InvoiceNumber invoiceNumber,
            string filename
        )
        {
            return new StoredDocument
            {
                DocumentId = Guid.NewGuid(),
                InvoiceNumber = invoiceNumber,
                IsPrimaryDocument = true,
                MimeType = "application/pdf",
                Filename = filename
            };
        }

        public static StoredDocument CreateAttachment(
            InvoiceNumber invoiceNumber,
            string filename,
            string mimeType
        )
        {
            return new StoredDocument
            {
                DocumentId = Guid.NewGuid(),
                InvoiceNumber = invoiceNumber,
                IsPrimaryDocument = false,
                MimeType = mimeType,
                Filename = filename
            };
        }
    }
}
