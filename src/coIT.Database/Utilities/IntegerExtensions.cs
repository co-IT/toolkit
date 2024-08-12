using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coIT.Database.Utilities
{
    internal static class IntegerExtensions
    {
        public static int[] AsDigits(this int num)
        {
            List<int> listOfInts = new List<int>();

            while (num > 0)
            {
                listOfInts.Add(num % 10);
                num = num / 10;
            }
            listOfInts.Reverse();

            return listOfInts.ToArray();
        }

        public static string GetMonthName(this int month)
        {
            var tempDate = new DateTime(2000, month, 1);

            return tempDate.ToString("MMMM", CultureInfo.CreateSpecificCulture("DE-de"));
        }

        public static int GetQuarterFromMonth(this int month)
        {
            var monthByQuarter = month / 4d;

            if (monthByQuarter < 1)
                return 1;

            if (monthByQuarter <= 1.5)
                return 2;

            if (monthByQuarter <= 2.25)
                return 3;

            if (monthByQuarter <= 3)
                return 4;

            throw new ArgumentOutOfRangeException(nameof(month), "Kein g�ltiger Monat!");
        }
    }
}
