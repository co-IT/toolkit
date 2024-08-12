using coIT.Database.Entities.ValueObjects;
using CSharpFunctionalExtensions;
using LiteDB;

namespace coIT.Database.Entities
{
    public class Calculation
    {
        public int Number { get; private set; }

        [BsonRef("ControllingAccount")]
        public ControllingAccount Account { get; private set; }

        [BsonRef("Employee")]
        public Employee Employee { get; private set; }

        public Amount Amount { get; private set; }

        public bool ModifiedManually { get; private set; }

        public Amount ConsultingRevenueOfMarketDivision
        {
            get
            {
                if (!Account.IsConsulting)
                    return Amount.Zero();

                if (Employee.OnlyMarket)
                    return Amount;

                return Account.IsFiscalUnity ? Amount.Zero() : Amount;
            }
        }

        public Amount ConsultingRevenueOfFiscalUnityDivision
        {
            get
            {
                if (!Account.IsConsulting)
                    return Amount.Zero();

                if (Employee.OnlyMarket)
                    return Amount.Zero();

                return Account.IsFiscalUnity ? Amount : Amount.Zero();
            }
        }

        public Calculation(int number, ControllingAccount account, Employee employee, Amount amount)
        {
            if (number < 0)
                throw new ArgumentOutOfRangeException(
                    $"Positionsnummer darf nicht kleiner als 0 sein, {nameof(number)}"
                );

            Number = number;
            Account = account ?? throw new ArgumentNullException(nameof(account));
            Employee = employee ?? throw new ArgumentNullException(nameof(employee));
            Amount = amount;
        }

        //Needed for DBLite
        public Calculation() { }

        public Result ChangeNumber(int number)
        {
            if (number < 0)
                return Result.Failure("Die Nummer muss mindestens 0 sein.");

            Number = number;

            ModifiedManually = true;

            return Result.Ok();
        }

        public Result FixWrongAccount(ControllingAccount account)
        {
            if (account is null)
                return Result.Failure("Neuer Account ungültig.");

            Account = account;

            ModifiedManually = true;

            return Result.Ok();
        }

        public Result Reverse()
        {
            Amount = new Amount(0, '€');
            ModifiedManually = true;

            return Result.Ok();
        }

        public Result IsCanceled() => Reverse();
    }
}
