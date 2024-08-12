using coIT.Libraries.Clockodo.TimeEntries.Contracts;
using CSharpFunctionalExtensions;

namespace coIT.Libraries.Clockodo.BusinessRules.TimeEntryOnDayRules
{
    internal class DailyTimeEntriesDontOverlap : IDailyTimeEntriesRule
    {
        public Result<HashSet<TimeEntry>, ClockodoFailure> Evaluate(
            HashSet<TimeEntry> timeEntriesOnDay
        )
        {
            var orderedTimeEntries = timeEntriesOnDay.OrderBy(entry => entry.Start).ToList();

            var overlapped = OverlapPresentInSortedEntries(orderedTimeEntries);

            return Result.FailureIf(
                overlapped,
                timeEntriesOnDay,
                ClockodoFailure.FromTimeEntriesOnDay(
                    timeEntriesOnDay,
                    "Hier liegt eine Überschneidung vor, bitte aktualisiere die Zeiteinträge",
                    ClockodoFailureType.PendingChangeRequest
                )
            );
        }

        private bool OverlapPresentInSortedEntries(List<TimeEntry> orderedTimeEntries)
        {
            var overlapsPresent = false;
            for (var entryNumber = 0; entryNumber < orderedTimeEntries.Count - 1; entryNumber++)
            {
                var formerEntry = orderedTimeEntries[entryNumber];
                var followingEntry = orderedTimeEntries[entryNumber + 1];

                if (TimeEntriesOverlap(formerEntry, followingEntry))
                    overlapsPresent = true;
            }
            return overlapsPresent;
        }

        private bool TimeEntriesOverlap(TimeEntry entryOne, TimeEntry entryTwo)
        {
            return entryTwo.Start < entryOne.End;
        }
    }
}
