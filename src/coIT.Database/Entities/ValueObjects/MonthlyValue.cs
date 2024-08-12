namespace coIT.Database.Entities.ValueObjects
{
    public class MonthlyValue
    {
        public Month Month { get; private set; }
        public decimal Value { get; private set; }

        public MonthlyValue(Month month, decimal value)
        {
            if (month is null)
                throw new ArgumentNullException(nameof(month), "Monat darf nicht null sein");

            Month = month;
            Value = value;
        }

        public MonthlyValue() { }

        public static implicit operator int(MonthlyValue mv) => mv.Month;

        public static implicit operator decimal(MonthlyValue mv) => mv.Value;
    }
}
