namespace coIT.Database.Entities
{
    public class DebitoorError : Entity
    {
        public string DebitoorId { get; set; }
        public int PersonnelNumber { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }
        public string InvoiceNumber { get; set; }
    }
}
