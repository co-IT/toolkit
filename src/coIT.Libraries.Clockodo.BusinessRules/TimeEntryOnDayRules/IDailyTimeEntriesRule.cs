using coIT.Libraries.Clockodo.TimeEntries.Contracts;
using CSharpFunctionalExtensions;

namespace coIT.Libraries.Clockodo.BusinessRules.TimeEntryOnDayRules
{
    internal interface IDailyTimeEntriesRule
    {
        public Result<HashSet<TimeEntry>, ClockodoFailure> Evaluate(
            HashSet<TimeEntry> changeRequest
        );
    }
}
