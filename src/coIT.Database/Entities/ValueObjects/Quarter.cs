namespace coIT.Database.Entities.ValueObjects
{
    public class Quarter
    {
        public decimal Value { get; private set; }
        public int Index { get; private set; }
        public int Year { get; private set; }

        public Quarter() { }

        public Quarter(decimal value, int index, int year)
        {
            if (index < 1 || index > 4)
                throw new ArgumentOutOfRangeException(
                    $"Quartal darf muss zwischen 1 und 4 liegen {index}"
                );

            if (year < 1900 || year > 3000)
                throw new ArgumentOutOfRangeException($"Jahr ist falsch: {year}");

            Value = value;
            Index = index;
            Year = year;
        }
    }
}
