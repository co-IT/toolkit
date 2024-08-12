namespace coIT.Database.Entities.ValueObjects
{
    public class Amount : IEquatable<Amount>
    {
        public decimal Value { get; private set; }
        public char CurrencySymbol { get; private set; }

        public Amount(decimal value, char currencySymbol)
        {
            if (currencySymbol != '\u20AC' && currencySymbol != '\u0024')
                throw new ArgumentOutOfRangeException(
                    $"Währungssymbol muss Dollar oder Euro entsprechen: {currencySymbol}"
                );

            Value = value;
            CurrencySymbol = currencySymbol;
        }

        public Amount() { }

        public static implicit operator decimal(Amount amount) => amount.Value;

        public static implicit operator char(Amount amount) => amount.CurrencySymbol;

        public static Amount operator +(Amount amount) => amount;

        public static Amount operator -(Amount amount) =>
            new Amount(amount.Value, amount.CurrencySymbol);

        public static Amount operator +(Amount a, Amount b) =>
            a.CurrencySymbol == b.CurrencySymbol
                ? new Amount(a.Value + b.Value, a.CurrencySymbol)
                : throw new InvalidOperationException("amounts have differenct currencies");

        public static Amount operator -(Amount a, Amount b) =>
            a.CurrencySymbol == b.CurrencySymbol
                ? new Amount(a.Value - b.Value, a.CurrencySymbol)
                : throw new InvalidOperationException("amounts have differenct currencies");

        public static Amount operator *(Amount a, Amount b) =>
            a.CurrencySymbol == b.CurrencySymbol
                ? new Amount(a.Value * b.Value, a.CurrencySymbol)
                : throw new InvalidOperationException("amounts have differenct currencies");

        public static Amount operator /(Amount a, Amount b) =>
            a.CurrencySymbol == b.CurrencySymbol
                ? new Amount(a.Value / b.Value, a.CurrencySymbol)
                : throw new InvalidOperationException("amounts have differenct currencies");

        public static Amount InEuro(decimal value) => new Amount(value, '€');

        public static Amount Zero() => InEuro(0);

        public override string ToString() => $"{Value:N} {CurrencySymbol}";

        public bool Equals(Amount other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            return Value == other.Value && CurrencySymbol == other.CurrencySymbol;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != this.GetType())
                return false;
            return Equals((Amount)obj);
        }

        public override int GetHashCode() => HashCode.Combine(Value, CurrencySymbol);
    }
}
