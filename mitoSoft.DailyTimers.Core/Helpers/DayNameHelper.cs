using System;
using System.Linq;

namespace mitoSoft.DailyTimers.Core.Helpers
{
    internal static class DayNameHelper
    {
        public static string ToShortestDayName(int dayIndex)
        {
            var dayNames = System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat.AbbreviatedDayNames.ToList();

            if (dayIndex == 7)
            {
                return dayNames[0];
            }
            else if (dayIndex >= 0 && dayIndex <= 6)
            {
                return dayNames[dayIndex];
            }
            else
            {
                throw new InvalidCastException($"DayIndex {dayIndex} not castable.");
            }
        }
    }
}