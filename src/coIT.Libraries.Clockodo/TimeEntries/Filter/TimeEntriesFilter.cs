using coIT.Libraries.Clockodo.TimeEntries.Contracts;

namespace coIT.Libraries.Clockodo.TimeEntries.Filter;

public static class TimeEntriesFilter
{
    public static IEnumerable<TimeEntry> ApplyAllFilters(
        this IEnumerable<TimeEntry> source,
        IEnumerable<IFilterTimeEntries> filters
    )
    {
        var filteredEntries = source;

        foreach (var filter in filters)
            filteredEntries = filter.Filter(filteredEntries);

        return filteredEntries;
    }
}
