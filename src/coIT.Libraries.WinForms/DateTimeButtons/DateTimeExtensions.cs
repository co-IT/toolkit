namespace coIT.Libraries.WinForms.DateTimeButtons
{
    public static class DateTimeExtensions
    {
        public static int GetQuarter(this DateTime date)
        {
            if (date.Month >= 4 && date.Month <= 6)
                return 2;
            if (date.Month >= 7 && date.Month <= 9)
                return 3;
            if (date.Month >= 10 && date.Month <= 12)
                return 4;
            return 1;
        }

        public static int GetLastFinishedMonthOfThisYear(this DateTime date)
        {
            return date.Month == 1 ? 1 : date.Month - 1;
        }

        public static int GetLastFinishedMonthOfYear(this DateTime date, int year)
        {
            if (year < date.Year)
                return 12;

            return date.Month == 1 ? 1 : date.Month - 1;
        }

        public static bool IsOnSameDay(this DateTime date1, DateTime date2)
        {
            return date1.Date == date2.Date;
        }

        public static bool IsInPeriod(this DateTime date, DateTime dateFrom, DateTime dateTo)
        {
            if (date.Date < dateFrom.Date)
                return false;
            if (date.Date > dateTo.Date)
                return false;
            return true;
        }

        public static DateTime GetFirstDayInMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        public static DateTime GetLastDayInMonth(this DateTime date)
        {
            var dateNextMonth = date.AddMonths(1);
            var firstDayNextMonth = new DateTime(dateNextMonth.Year, dateNextMonth.Month, 1);

            return firstDayNextMonth.AddDays(-1);
        }

        public static DateTime GetEndOfDay(this DateTime day)
        {
            var lengthOfday = TimeSpan.FromDays(1);
            var shortestTimeUnit = TimeSpan.FromTicks(1);

            return day.Date + lengthOfday - shortestTimeUnit;
        }

        public static DateTime Max(this DateTime date1, DateTime date2)
        {
            return date1 < date2 ? date2 : date1;
        }

        public static DateOnly ToDateOnly(this DateTime dateTime)
        {
            return DateOnly.FromDateTime(dateTime);
        }
    }
}
