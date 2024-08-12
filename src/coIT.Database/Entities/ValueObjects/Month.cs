using Newtonsoft.Json;

namespace coIT.Database.Entities.ValueObjects
{
    public class Month
    {
        public int Value { get; private set; }

        [JsonConstructor]
        public Month(int value)
        {
            if (value > 12 || value < 1)
                throw new ArgumentOutOfRangeException();

            Value = value;
        }

        public Month() { }

        public static implicit operator int(Month m) => m.Value;
    }
}
