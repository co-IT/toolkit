using coIT.Database.Entities.ValueObjects;
using CSharpFunctionalExtensions;

namespace coIT.Database.Entities
{
    public class PersonnelCosts : Entity
    {
        public DateTime Date { get; private set; }
        public AbsoluteAmount AmountMarket { get; private set; }
        public AbsoluteAmount AmountFiscalUnityScheme { get; private set; }

        public PersonnelCosts() { }

        public PersonnelCosts(
            DateTime date,
            AbsoluteAmount amountMarket,
            AbsoluteAmount amountFiscalUnityScheme
        )
        {
            Date = date;
            AmountMarket = amountMarket;
            AmountFiscalUnityScheme = amountFiscalUnityScheme;
        }

        public Result ChangeDate(int year, int month)
        {
            try
            {
                Date = new DateTime(year, month, 1);

                return Result.Ok();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return Result.Failure(ex.Message);
            }
        }

        public Result ChangeAmountMarket(decimal amount)
        {
            try
            {
                AmountMarket = new AbsoluteAmount(amount, AmountMarket.CurrencySymbol);

                return Result.Ok();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return Result.Failure(ex.Message);
            }
        }

        public Result ChangeAmountFiscalUnityScheme(decimal amount)
        {
            try
            {
                AmountFiscalUnityScheme = new AbsoluteAmount(
                    amount,
                    AmountFiscalUnityScheme.CurrencySymbol
                );

                return Result.Ok();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return Result.Failure(ex.Message);
            }
        }
    }
}
