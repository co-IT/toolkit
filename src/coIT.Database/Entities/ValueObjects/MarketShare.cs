namespace coIT.Database.Entities.ValueObjects
{
    public class MarketShare
    {
        public MarketShare(double percentage)
        {
            if (percentage < 0)
                throw new ArgumentOutOfRangeException(
                    $"Prozentsatz darf nicht negativ sein {percentage}"
                );

            if (percentage > 1)
                throw new ArgumentOutOfRangeException(
                    $"Prozentsatz darf nicht über 100 liegen {percentage}"
                );

            Percentage = percentage;
        }

        public MarketShare() { }

        public double Percentage { get; private set; }
    }
}
