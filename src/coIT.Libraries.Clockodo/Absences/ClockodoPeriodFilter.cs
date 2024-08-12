using System.Collections.Immutable;
using CSharpFunctionalExtensions;

namespace coIT.Libraries.Clockodo.Absences
{
    public class ClockodoPeriodFilter
    {
        internal Func<DateSpan, bool> Predicate { get; }

        internal string Uri { get; }

        public DateTime Start { get; }

        public DateTime End { get; }

        public DateSpan Period => new DateSpan(Start, End);

        private ClockodoPeriodFilter(DateTime fromIncluding, DateTime toIncluding)
        {
            //todo: tatsächlichen use case darstellen. Muss die Zeit vor der Periode begonnen haben oder abgeschlossen sein?
            Predicate = timeEntry =>
                timeEntry.Start >= fromIncluding && timeEntry.End <= toIncluding;

            Uri =
                $"time_since={fromIncluding:yyyy-MM-dd}%20{fromIncluding:hh:mm:ss}&time_until={toIncluding:yyyy-MM-dd}%20{toIncluding:hh:mm:ss}";

            Start = fromIncluding;
            End = toIncluding;
        }

        public static Result<ClockodoPeriodFilter> Create(FetchFilterDto requested)
        {
            if (
                requested == null
                || string.IsNullOrWhiteSpace(requested.To)
                || string.IsNullOrWhiteSpace(requested.From)
            )
                return Result.Failure<ClockodoPeriodFilter>("Unerlaubter Zeitraum");

            if (!DateTime.TryParse(requested.From, out var fromIncluding))
                return Result.Failure<ClockodoPeriodFilter>("Unerlaubter Zeitraum");

            if (!DateTime.TryParse(requested.To, out var toIncluding))
                return Result.Failure<ClockodoPeriodFilter>("Unerlaubter Zeitraum");

            return Create(fromIncluding, toIncluding);
        }

        public static Result<ClockodoPeriodFilter> Create(DateTime from, DateTime to)
        {
            var minDate = new DateTime(2020, 1, 1);
            if (from < minDate || to < minDate)
                return Result.Failure<ClockodoPeriodFilter>(
                    "Das Startdatum muss größer sein als 01.01.2020"
                );

            if (from >= to)
                return Result.Failure<ClockodoPeriodFilter>(
                    "Das Startdatum muss größer sein als das Enddatum"
                );

            return Result.Success(new ClockodoPeriodFilter(from, to));
        }

        public bool IsInPeriod(DateTime date) => date.IsInPeriod(Start, End);

        public IImmutableList<int> YearsOfPeriod
        {
            get
            {
                var years = new List<int>();

                var startYear = Start.Year;
                var endYear = End.Year;

                for (var year = startYear; year <= endYear; year++)
                    years.Add(year);

                return years.ToImmutableList();
            }
        }
    }
}
