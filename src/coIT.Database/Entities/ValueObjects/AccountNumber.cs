using coIT.Database.Utilities;

namespace coIT.Database.Entities.ValueObjects
{
    public class AccountNumber
    {
        public int Number { get; private set; }

        public AccountNumber() { }

        public AccountNumber(int number)
        {
            if (number.AsDigits().Length > 4 || number.AsDigits().Length < 2)
                throw new ArgumentOutOfRangeException(
                    $"Kontonummer muss 2 bis 4-stellig sein {number}"
                );

            Number = number;
        }

        public static implicit operator int(AccountNumber accountNumber) => accountNumber.Number;
    }
}
