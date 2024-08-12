using coIT.Libraries.Clockodo.TimeEntries.Contracts;
using CSharpFunctionalExtensions;

namespace coIT.Libraries.Clockodo.BusinessRules.TimeEntryRules
{
    internal class TimeTimeEntryRuleTextRule : IClockodoTimeEntryRule
    {
        private Result<TimeEntry, ClockodoFailure> TextNotNullOrEmpty(TimeEntry timeEntry)
        {
            return Result.SuccessIf(
                !string.IsNullOrWhiteSpace(timeEntry.Text),
                timeEntry,
                ClockodoFailure.FromClockodoTimeEntry(
                    timeEntry,
                    $"Füge dem Zeiteintrag eine Beschreibung hinzu",
                    ClockodoFailureType.TimeEntryNoText
                )
            );
        }

        private Result<TimeEntry, ClockodoFailure> TextMoreThan3CharactersRule(TimeEntry timeEntry)
        {
            return Result.FailureIf(
                timeEntry.Text.Trim().Length < 3,
                timeEntry,
                ClockodoFailure.FromClockodoTimeEntry(
                    timeEntry,
                    $"Aktualisiere den Zeiteintrag mit einer längere Beschreibung",
                    ClockodoFailureType.TimeEntryTextToShort
                )
            );
        }

        public Result<TimeEntry, ClockodoFailure> Evaluate(TimeEntry timeEntry)
        {
            return TextNotNullOrEmpty(timeEntry).Bind(_ => TextMoreThan3CharactersRule(timeEntry));
        }
    }
}
