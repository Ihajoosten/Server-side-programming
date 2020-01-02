using Itenso.TimePeriod;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Cook.Extensions
{
    public static class Methods
    {
        public static DateTime GetDateFromWeekNumberAndDayOfWeek(this int weekNumber, int dayOfWeek, int year)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = DayOfWeek.Tuesday - jan1.DayOfWeek;

            DateTime firstMonday = jan1.AddDays(daysOffset);

            var cal = CultureInfo.CurrentCulture.Calendar;
            int firstWeek = cal.GetWeekOfYear(jan1, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            var weekNum = weekNumber;
            if (firstWeek <= 1)
            {
                weekNum -= 1;
            }

            var result = firstMonday.AddDays(weekNum * 7 + dayOfWeek - 1);
            return result;
        }
    }
}
