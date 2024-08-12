using coIT.Libraries.Clockodo.TimeEntries.Contracts;
using CSharpFunctionalExtensions;

namespace coIT.Libraries.Clockodo.BusinessRules.TimeEntryRules
{
    internal class TimeTimeEntryRuleDurationRule : IClockodoTimeEntryRule
    {
        private Result<TimeEntry, ClockodoFailure> Evaluate10MinuteRule(TimeEntry timeEntry)
        {
            return Result.SuccessIf(
                timeEntry.Duration >= 10 * 60,
                timeEntry,
                ClockodoFailure.FromClockodoTimeEntry(
                    timeEntry,
                    $"Bitte stell sicher, dass ein Zeiteintrag mindestens 10 Minuten lang ist",
                    ClockodoFailureType.TimeEntryShorterThan10Minutes
                )
            );
        }

        public Result<TimeEntry, ClockodoFailure> Evaluate(TimeEntry timeEntry) =>
            Evaluate10MinuteRule(timeEntry);
    }
}
