using coIT.Database.Entities.ValueObjects;
using coIT.Database.Utilities;
using CSharpFunctionalExtensions;

namespace coIT.Database.Entities
{
    public class CalculationGroup : Entity
    {
        public DateTime Date { get; private set; }

        public InvoiceNumber Number { get; private set; }

        public IEnumerable<Calculation> Calculations { get; private set; }

        public Debitor Debitor { get; private set; }

        public Origin Origin { get; private set; }

        public bool ModifiedManually { get; private set; }

        public bool HasModifications =>
            ModifiedManually || Calculations.Any(line => line.ModifiedManually);

        public Amount ConsultingRevenueOfMarketDivision =>
            Calculations.Aggregate(
                Amount.Zero(),
                (currentAmount, newAmount) =>
                    currentAmount + newAmount.ConsultingRevenueOfMarketDivision
            );

        public Amount ConsultingRevenueOfFiscalUnityDivision =>
            Calculations.Aggregate(
                Amount.Zero(),
                (currentAmount, newAmount) =>
                    currentAmount + newAmount.ConsultingRevenueOfFiscalUnityDivision
            );

        public CalculationGroup() => Calculations = new List<Calculation>();

        public CalculationGroup(InvoiceNumber number, DateTime date, Debitor debitor, Origin origin)
        {
            Date = date;
            Number = number ?? throw new ArgumentNullException(nameof(number));
            Debitor = debitor ?? throw new ArgumentNullException(nameof(debitor));
            Origin = origin ?? throw new ArgumentNullException(nameof(origin));
            Calculations = new List<Calculation>();
        }

        public Result FixCalculationPeriod(DateTime newDate)
        {
            var minDate = new DateTime(2016, 3, 3);
            var maxDate = DateTime.Now;

            if (newDate < minDate || newDate > maxDate)
                return Result.Failure($"Das Datum muss zw. {minDate} und {maxDate} liegen.");

            Date = newDate;

            ModifiedManually = true;

            return Result.Ok();
        }

        public void AddCalculation(ControllingAccount account, Employee consultant, Amount amount)
        {
            if (consultant is null)
                throw new ArgumentNullException(nameof(consultant));

            if (account is null)
                throw new ArgumentNullException(nameof(account));

            if (amount is null)
                throw new ArgumentNullException(nameof(amount));

            var nextNumber = 1;

            if (Calculations.Any())
                nextNumber += Calculations.Max(calculation => calculation.Number);

            var newCalculation = new Calculation(nextNumber, account, consultant, amount);

            Calculations = Calculations.Concat(new[] { newCalculation });
        }

        public Result MarkAsStorno()
        {
            var results = new List<Result>();

            foreach (var calculation in Calculations)
                results.Add(calculation.Reverse());

            ModifiedManually = true;

            if (results.Any(result => result.IsFailure))
                return Result.Failure(results.ContentToString());

            return Result.Ok();
        }

        public Result MarkAsCanceled()
        {
            var results = new List<Result>();

            foreach (var calculation in Calculations)
                results.Add(calculation.IsCanceled());

            ModifiedManually = true;

            if (results.Any(result => result.IsFailure))
                return Result.Failure(results.ContentToString());

            return Result.Ok();
        }
    }
}
