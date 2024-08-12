namespace coIT.Libraries.Clockodo.Absences;

public class DateSpan
{
    public DateSpan() { }

    public DateSpan(DateTime start, DateTime end)
    {
        if (start.Year < 1900 || start.Year > 3000)
            throw new ArgumentOutOfRangeException($"Zeitspanne ist nicht gültig {start.Year}");

        if (end.Year < 1900 || end.Year > 3000)
            throw new ArgumentOutOfRangeException($"Zeitspanne ist nicht gültig {end.Year}");

        if (start > end)
            throw new ArgumentOutOfRangeException(
                $"Startdatum darf nicht nach Enddatum liegen {start}, {end}"
            );

        Start = start;
        End = end;
    }

    public DateTime Start { get; }
    public DateTime End { get; }

    public List<DateSpan> SplitAtMonthEnd()
    {
        var monthsInBetween = Math.Abs(12 * (Start.Year - End.Year) + Start.Month - End.Month);

        if (monthsInBetween == 0)
            return new List<DateSpan> { this };

        var dateSpans = new List<DateSpan>();

        var lastDateSpanEnd = Start.AddDays(-1);

        while (dateSpans.Count < monthsInBetween + 1)
        {
            var currentSpanStart = lastDateSpanEnd.AddDays(1);
            lastDateSpanEnd =
                dateSpans.Count < monthsInBetween ? currentSpanStart.GetLastDayInMonth() : End;
            dateSpans.Add(new DateSpan(currentSpanStart, lastDateSpanEnd));
        }

        return dateSpans;
    }

    public bool SpansOverAMonth()
    {
        return SplitAtMonthEnd().Count > 1;
    }

    public bool IntersectsWith(DateSpan otherSpan)
    {
        return otherSpan.Contains(Start)
            || otherSpan.Contains(End)
            || Contains(otherSpan.Start)
            || Contains(otherSpan.End);
    }

    public bool Contains(DateTime day)
    {
        return day.IsInPeriod(Start, End);
    }

    public bool Contains(DateSpan containedPeriod)
    {
        return containedPeriod.Start.IsInPeriod(Start, End)
            && containedPeriod.End.IsInPeriod(Start, End);
    }

    public override string ToString()
    {
        return $"{Start.ToShortDateString()} bis {End.ToShortDateString()}";
    }
}
