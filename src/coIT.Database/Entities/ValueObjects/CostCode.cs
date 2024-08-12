namespace coIT.Database.Entities.ValueObjects
{
    public class CostCode
    {
        public double Percentage { get; private set; }

        public CostCode(double percentage)
        {
            if (percentage < 0 || percentage > 100)
                throw new ArgumentOutOfRangeException(nameof(percentage));

            Percentage = percentage;
        }

        public CostCode() { }

        public static implicit operator double(CostCode c) => c.Percentage;

        public static implicit operator decimal(CostCode c) => Convert.ToDecimal(c.Percentage);
    }
}
