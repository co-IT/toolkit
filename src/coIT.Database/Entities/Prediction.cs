using coIT.Database.Entities.ValueObjects;
using coIT.Database.Utilities;
using CSharpFunctionalExtensions;

namespace coIT.Database.Entities
{
    public class Prediction : Entity
    {
        public Year Year { get; private set; }
        public Amount ExpectedPersonnelCosts { get; private set; }
        public decimal ExpectedPercentageSurplus { get; private set; }
        public decimal ExpectedPercentageOperatingCosts { get; private set; }
        public decimal ExpectedPercentageOfRevenuesByMarket { get; private set; }

        public Prediction() { }

        public Prediction(
            Year year,
            Amount expectedPersonnelCosts,
            decimal expectedPercentageSurplus,
            decimal expectedPercentageOperatingCosts,
            decimal expectedPercentageOfRevenuesByMarket
        )
        {
            Year = year;
            ExpectedPersonnelCosts = expectedPersonnelCosts;

            var results = new List<Result>
            {
                PredictPercentageSurplus(expectedPercentageSurplus),
                PredictPercentageOperatingCosts(expectedPercentageOperatingCosts),
                PredictPercentageOfRevenuesByMarket(expectedPercentageOfRevenuesByMarket)
            };

            if (results.Any(result => result.IsFailure))
                throw new ArgumentException(results.ContentToString());
        }

        public Result PredictPercentageOfRevenuesByMarket(decimal percentageOfRevenuesByMarket)
        {
            if (percentageOfRevenuesByMarket <= 0 || percentageOfRevenuesByMarket >= 100)
                return Result.Failure(
                    "Anteil GS Markt darf nicht kleiner oder gleich 0 sein.\nAnteil Markt darf nicht größer oder gleich 100 sein."
                );

            ExpectedPercentageOfRevenuesByMarket = percentageOfRevenuesByMarket;

            return Result.Ok();
        }

        public Result PredictPersonnelCosts(Amount personnelCosts)
        {
            if (personnelCosts.Value < 100000)
                return Result.Failure("Personalaufwand muss größer 100.000€ sein.");

            ExpectedPersonnelCosts = personnelCosts;

            return Result.Ok();
        }

        public Result PredictPercentageSurplus(decimal percentageSurplus)
        {
            if (percentageSurplus <= 0)
                return Result.Failure("Personalquote muss größer 0 sein.");

            ExpectedPercentageSurplus = percentageSurplus;

            return Result.Ok();
        }

        public Result PredictPercentageOperatingCosts(decimal percentageOperatingCosts)
        {
            if (percentageOperatingCosts <= 0)
                return Result.Failure("Prozent der betrieblichen Kosten muss größer 0 sein.");

            ExpectedPercentageOperatingCosts = percentageOperatingCosts;

            return Result.Ok();
        }
    }
}
