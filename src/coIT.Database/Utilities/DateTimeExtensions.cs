using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coIT.Database.Utilities
{
    internal static class DateTimeExtensions
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

        public static int GetLastFinishedMonthOfThisYear(this DateTime date) =>
            date.Month == 1 ? 1 : date.Month - 1;

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

        public static DateTime Max(this DateTime date1, DateTime date2)
        {
            return date1 < date2 ? date2 : date1;
        }

        public static DateTime SetTime(
            this DateTime date,
            int hour = 0,
            int minute = 0,
            int second = 0,
            int millisecond = 0
        )
        {
            return new DateTime(date.Year, date.Month, date.Day, hour, minute, second, millisecond);
        }
    }
}
