namespace coIT.Database.Entities.ValueObjects
{
    public class AbsoluteAmount
    {
        public decimal Value { get; private set; }
        public char CurrencySymbol { get; private set; }

        public AbsoluteAmount(decimal value, char currencySymbol)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException($"Betrag muss positiv sein: {value}");

            if (currencySymbol != '\u20AC' && currencySymbol != '\u0024')
                throw new ArgumentOutOfRangeException(
                    $"Währungssymbol muss Dollar oder Euro entsprechen: {currencySymbol}"
                );

            Value = value;
            CurrencySymbol = currencySymbol;
        }

        public AbsoluteAmount() { }

        public static implicit operator decimal(AbsoluteAmount amount) => amount.Value;

        public static implicit operator char(AbsoluteAmount amount) => amount.CurrencySymbol;
    }
}
