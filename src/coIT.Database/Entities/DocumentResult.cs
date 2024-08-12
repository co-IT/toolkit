namespace coIT.Database.Entities
{
    public class DocumentResult
    {
        public Uri Uri { get; }
        public byte[] Binary { get; }

        public DocumentResult(Uri uri, byte[] binary)
        {
            Uri = uri;
            Binary = binary;
        }
    }
}
