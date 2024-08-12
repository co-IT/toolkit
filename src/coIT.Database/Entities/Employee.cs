using coIT.Database.Entities.ValueObjects;
using CSharpFunctionalExtensions;

namespace coIT.Database.Entities
{
    public class Employee : Entity
    {
        public Employee() { }

        public Employee(
            Year year,
            FullName name,
            PersonnelNumber personnelNumber,
            CostCode costCode,
            PositiveInteger workdays,
            bool onlyMarket,
            bool importFromTimeTrackingSystem,
            bool ignoreWorkingHoursViolations,
            bool ignoreInStatistics
        )
        {
            Year = year;
            Name = name;
            PersonnelNumber = personnelNumber;
            CostCode = costCode;
            Workdays = workdays;
            OnlyMarket = onlyMarket;
            ImportFromTimeTrackingSystem = importFromTimeTrackingSystem;
            IgnoreWorkingHoursViolations = ignoreWorkingHoursViolations;
            IgnoreInStatistics = ignoreInStatistics;
        }

        public Year Year { get; private set; }
        public FullName Name { get; private set; }
        public PersonnelNumber PersonnelNumber { get; private set; }
        public CostCode CostCode { get; private set; }
        public PositiveInteger Workdays { get; private set; }
        public bool OnlyMarket { get; private set; }
        public bool ImportFromTimeTrackingSystem { get; private set; }
        public bool IgnoreWorkingHoursViolations { get; private set; }
        public bool IgnoreInStatistics { get; private set; }

        public Result MoveToYear(Year year)
        {
            Year = year;

            return Result.Ok();
        }

        public Result ChangeNumber(PersonnelNumber personnelNumber)
        {
            PersonnelNumber = personnelNumber;

            return Result.Ok();
        }

        public Result ChangeWorkdays(PositiveInteger workdays)
        {
            Workdays = workdays;

            return Result.Ok();
        }

        public Result ChangeCostCode(CostCode costCode)
        {
            CostCode = costCode;

            return Result.Ok();
        }

        public Result ChangeBusinessDivision(bool onlyMarket)
        {
            OnlyMarket = onlyMarket;

            return Result.Ok();
        }

        public Result ChangeWorkingViolationsPolicy(bool ignoreWorkingHoursViolations)
        {
            IgnoreWorkingHoursViolations = ignoreWorkingHoursViolations;

            return Result.Ok();
        }

        public Result ChangeTimeTrackingImportPolicy(bool importFromTimeTrackingSystem)
        {
            ImportFromTimeTrackingSystem = importFromTimeTrackingSystem;

            return Result.Ok();
        }

        public Result ChangeName(FullName name)
        {
            Name = name;

            return Result.Ok();
        }

        public Result ChangeStatisticsPolicy(bool ignoreInStatistics)
        {
            IgnoreInStatistics = ignoreInStatistics;

            return Result.Ok();
        }
    }
}
