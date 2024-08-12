using CSharpFunctionalExtensions;

namespace coIT.Database.Entities.ValueObjects
{
    public class TaxKey
    {
        public byte Number { get; private set; }

        public TaxKey(byte number)
        {
            if (!IsValid(number))
                throw new ArgumentOutOfRangeException(
                    $"Der Steuerschlüssel darf nur 0, 4 oder 5 sein {number}"
                );

            Number = number;
        }

        public TaxKey() { }

        public static Result<TaxKey> Create(byte number)
        {
            if (!IsValid(number))
                return Result.Failure<TaxKey>("Der Steuerschlüssel muss 0, 4 oder 5 sein.");

            return Result.Ok(new TaxKey(number));
        }

        public static implicit operator byte(TaxKey key) => key.Number;

        public static explicit operator TaxKey(int number)
        {
            var result = Create((byte)number);

            if (result.IsFailure)
                throw new InvalidOperationException("Kein gültiger Steuerschlüssel");

            return result.Value;
        }

        public static byte NoTaxes => 0;
        public static byte DefaultTaxes => 4;
        public static byte ReducedTaxes => 5;

        public override string ToString() => $"{Number}";

        private static bool IsValid(byte number) => number == 0 || number == 4 || number == 5;
    }
}
