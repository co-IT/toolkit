using coIT.Libraries.Clockodo.TimeEntries.Contracts;
using CSharpFunctionalExtensions;

namespace coIT.Libraries.Clockodo.BusinessRules.TimeEntryRules
{
    internal interface IClockodoTimeEntryRule
    {
        Result<TimeEntry, ClockodoFailure> Evaluate(TimeEntry timeEntry);
    }
}
