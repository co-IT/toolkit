namespace coIT.Database.Entities
{
    public class Document
    {
        public Guid Id { get; set; }

        public DateTime Date { get; set; }
        public int CustomerNumber { get; set; }
        public string DocumentNumber { get; set; }
        public byte[] Binary { get; set; }
        public string FileName { get; set; }
    }
}
