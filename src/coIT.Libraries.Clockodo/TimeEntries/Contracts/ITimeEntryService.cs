using System.Collections.Immutable;

namespace coIT.Libraries.Clockodo.TimeEntries.Contracts;

public interface ITimeEntriesService
{
    Task<IImmutableList<TimeEntry>> GetTimeEntriesAsync(ClockodoPeriod period, int employee);
}
