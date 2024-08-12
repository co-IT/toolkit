namespace coIT.Database.Entities.ValueObjects
{
    public class QuarterIndex
    {
        public int Number { get; }

        public QuarterIndex() { }

        public QuarterIndex(int number)
        {
            if (number < 1 || number > 4)
                throw new ArgumentOutOfRangeException(
                    $"Quartal darf muss zwischen 1 und 4 liegen {number}"
                );

            Number = number;
        }
    }
}
