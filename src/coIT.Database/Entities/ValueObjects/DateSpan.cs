using coIT.Database.Utilities;

namespace coIT.Database.Entities.ValueObjects
{
    public class DateSpan
    {
        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }

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

        public bool Contains(DateTime day) => day.IsInPeriod(Start, End);

        public bool Contains(DateSpan containedPeriod) =>
            containedPeriod.Start.IsInPeriod(Start, End)
            && containedPeriod.End.IsInPeriod(Start, End);

        public IEnumerable<int> GetYears()
        {
            var years = new List<int>();

            for (var year = Start.Year; year <= End.Year; year++)
            {
                years.Add(year);
            }

            return years;
        }

        public override string ToString() =>
            $"{Start.ToShortDateString()} bis {End.ToShortDateString()}";
    }
}
