using Newtonsoft.Json;

namespace coIT.Database.Entities.ValueObjects
{
    public class Year
    {
        public int Value { get; private set; }

        [JsonConstructor]
        public Year(int value)
        {
            if (value > 9999 || value < 1000)
                throw new ArgumentOutOfRangeException();

            Value = value;
        }

        public Year() { }

        public static implicit operator int(Year y) => y.Value;

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != GetType())
                return false;

            return Equals((Year)obj);
        }

        protected bool Equals(Year other) => Value == other.Value;

        public override int GetHashCode() => Value;
    }
}
