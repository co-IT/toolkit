namespace coIT.Database.Entities.ValueObjects
{
    public class BillableDays
    {
        public double Count { get; private set; }

        public BillableDays(double count)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(
                    $"Die Anzahl der fakturierbaren Tage darf nicht negativ sein!"
                );

            if (count > 31)
                throw new ArgumentOutOfRangeException($"");

            Count = count;
        }

        public BillableDays() { }

        public static implicit operator double(BillableDays w) => w.Count;
    }
}
