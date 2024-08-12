using coIT.Libraries.Clockodo.TimeEntries.Contracts;

namespace coIT.Libraries.Clockodo.TimeEntries.Filter;

public interface IFilterTimeEntries
{
    IEnumerable<TimeEntry> Filter(IEnumerable<TimeEntry> source);
}
