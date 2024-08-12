namespace coIT.Database.Entities.ValueObjects
{
    public class PositiveInteger
    {
        public int Value { get; private set; }

        public PositiveInteger(int value)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));

            Value = value;
        }

        public PositiveInteger() { }

        public static implicit operator int(PositiveInteger pI) => pI.Value;
    }
}
