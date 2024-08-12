using coIT.Libraries.Clockodo.TimeEntries.Contracts;

namespace coIT.Libraries.Clockodo.TimeEntries.Filter;

public class YearFilter : IFilterTimeEntries
{
    private readonly string _year;

    public YearFilter(string year)
    {
        _year = year;
    }

    public IEnumerable<TimeEntry> Filter(IEnumerable<TimeEntry> source)
    {
        return source.Where(entry => entry.Start.Year.ToString() == _year);
    }
}
