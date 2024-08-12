using coIT.Database.Utilities;

namespace coIT.Database.Entities.ValueObjects
{
    public class RevenueAccount
    {
        public RevenueAccount(int number, string name)
        {
            if (number.AsDigits().Length > 4 || number.AsDigits().Length < 2)
                throw new ArgumentOutOfRangeException(
                    $"Kontonummer muss 2 bis 4-stellig sein {number}"
                );

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentOutOfRangeException("Kontoname muss angegeben werden");

            Number = number;
            Name = name;
        }

        public RevenueAccount() { }

        public int Number { get; private set; }
        public string Name { get; private set; }

        public static implicit operator int(RevenueAccount ra) => ra.Number;

        public static implicit operator long(RevenueAccount ra) => ra.Number;

        public static implicit operator string(RevenueAccount ra) => ra.Name;

        public override string ToString() => Number.ToString();

        //todo: IEquatable implementieren
    }
}
