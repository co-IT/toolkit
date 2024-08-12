using System.Collections.Immutable;
using coIT.Libraries.Clockodo.Absences.Contracts;

namespace coIT.Libraries.Clockodo.Absences;

public interface IFilterAbsences
{
    IImmutableList<Absence> Filter(IImmutableList<Absence> absencesToFilter);
}

internal class YearFilter : IFilterAbsences
{
    private readonly int _year;

    public YearFilter(int year)
    {
        _year = year;
    }

    private string Uri => $"api/absences?year={_year}";

    public IImmutableList<Absence> Filter(IImmutableList<Absence> absencesToFilter)
    {
        return absencesToFilter
            .Where(a => a.Start.Year == _year || a.End.Year == _year)
            .ToImmutableList();
    }
}

internal class MonthFilter : IFilterAbsences
{
    private readonly int _month;

    public MonthFilter(int month)
    {
        _month = month;
    }

    public IImmutableList<Absence> Filter(IImmutableList<Absence> absencesToFilter)
    {
        return absencesToFilter
            .Where(a => a.Start.Month == _month || a.End.Month == _month)
            .ToImmutableList();
    }
}

internal class DateSpanFilter : IFilterAbsences
{
    private readonly DateSpan _dateSpan;

    public DateSpanFilter(DateSpan dateSpan)
    {
        _dateSpan = dateSpan;
    }

    public IImmutableList<Absence> Filter(IImmutableList<Absence> absencesToFilter)
    {
        var year = _dateSpan.End.Year;
        var month = _dateSpan.End.Month;

        IFilterAbsences yearFilter = new YearFilter(year);
        IFilterAbsences monthFilter = new MonthFilter(month);

        var absenceOfYear = yearFilter.Filter(absencesToFilter);
        var absenceOfYearAndMonth = monthFilter.Filter(absenceOfYear);

        return absenceOfYearAndMonth;
    }
}

public class ClockodoApiAdresses
{
    public string AllCustomers => "/api/customers/";
    public string AllEmployees => "/api/users/";
    public string AllServices => "/api/services/";

    public string AllTimeEntries(ClockodoPeriodFilter periodFilter, int pageNumber)
    {
        return $"/api/entries?{periodFilter.Uri}&page={pageNumber}";
    }

    public string UserReportsOfYear(int year)
    {
        return $"api/userreports?year={year}&type=4";
    }

    public string AbsencesOfYear(int year)
    {
        return $"api/absences?year={year}";
    }
}
