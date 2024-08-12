namespace coIT.Database.Entities
{
    public class DebitoorSync : Entity
    {
        public DateTime Date { get; private set; }

        public DebitoorSync(int id, DateTime date)
        {
            Id = id;
            Date = date;
        }

        public DebitoorSync() { }
    }
}
