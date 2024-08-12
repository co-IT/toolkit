using coIT.Database.Entities.ValueObjects;
using coIT.Database.Utilities;
using CSharpFunctionalExtensions;

namespace coIT.Database.Entities
{
    public class Milestone : Entity
    {
        public BusinessBranch BusinessBranch { get; private set; }
        public string GoalName { get; private set; }
        public AbsoluteAmount Amount { get; private set; }
        public DateTime Year { get; private set; }

        public Milestone() { }

        public Milestone(
            BusinessBranch businessBranch,
            string goalName,
            AbsoluteAmount amount,
            DateTime year
        )
        {
            BusinessBranch = businessBranch;
            GoalName = goalName;
            Amount = amount;
            Year = year;

            var results = new List<Result> { ChangeGoalName(goalName) };

            if (results.Any(result => result.IsFailure))
                throw new ArgumentException(results.ContentToString());
        }

        public Result ChangeGoalName(string goalName)
        {
            if (string.IsNullOrWhiteSpace(goalName))
                return Result.Failure("Der Zielname darf nicht leer sein.");

            GoalName = goalName;

            return Result.Ok();
        }

        public Result ChangeYear(int year)
        {
            try
            {
                Year = new DateTime(year, 1, 1);

                return Result.Ok();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return Result.Failure(ex.Message);
            }
        }

        public Result ChangeBusinessBranch(string name)
        {
            try
            {
                BusinessBranch = new BusinessBranch(name);

                return Result.Ok();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return Result.Failure(ex.Message);
            }
        }

        public Result ChangeAmount(decimal amount)
        {
            try
            {
                Amount = new AbsoluteAmount(amount, '€');

                return Result.Ok();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return Result.Failure(ex.Message);
            }
        }
    }
}
