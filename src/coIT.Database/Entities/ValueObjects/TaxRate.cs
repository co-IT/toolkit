namespace coIT.Database.Entities.ValueObjects
{
    public class TaxRate
    {
        public TaxRate(double rate)
        {
            if (rate < 0)
                throw new ArgumentOutOfRangeException($"Steuerrate darf nicht negativ sein {rate}");

            if (rate > 1)
                throw new ArgumentOutOfRangeException(
                    $"Steurrate darf nicht über 100% liegen {rate}"
                );

            Rate = rate;
        }

        public TaxRate() { }

        public double Rate { get; private set; }

        public static implicit operator double(TaxRate tr) => tr.Rate;
    }
}
