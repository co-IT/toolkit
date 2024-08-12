using coIT.Libraries.Clockodo.TimeEntries.Contracts;

namespace coIT.Libraries.Clockodo.TimeEntries.Filter;

internal class RunningEntryFilter
{
    public IEnumerable<RawTimeEntry> Filter(IEnumerable<RawTimeEntry> source)
    {
        return source.Where(rawEntry => !string.IsNullOrWhiteSpace(rawEntry.TimeUntil));
    }
}
