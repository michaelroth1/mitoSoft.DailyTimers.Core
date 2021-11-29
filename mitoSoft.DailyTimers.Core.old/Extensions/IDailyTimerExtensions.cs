using mitoSoft.DailyTimers.Core.Contracts;
using mitoSoft.DailyTimers.Core.Helpers;
using mitoSoft.DailyTimers.Core.Comparer;
using System.Collections.Generic;
using System.Linq;

namespace mitoSoft.DailyTimers.Core.Extensions
{
    public static class IDailyTimerExtensions
    {
        public static void Initialize(this IDailyTimer channel)
        {
            channel.Name = "Channel";
            channel.Active = false;
            channel.Description = "";
            channel.Monday = false;
            channel.Tuesday = false;
            channel.Wednesday = false;
            channel.Thursday = false;
            channel.Friday = false;
            channel.Saturday = true;
            channel.Sunday = true;
            channel.SwitchTime = "12:00";
            channel.IgnoreOnHolidays = true;
        }

        /// <summary>
        /// aus DaysOfWeek So/Mo/Di -> sowas So-Di machen
        /// </summary>
        public static string GetAbbreviationText(this IDailyTimer channel)
        {
            var daysOfWeek = GetDaysOfWeekList(channel);

            //find start
            var start = DayNameHelper.ToShortestDayName(daysOfWeek[0]);
            var startidx = daysOfWeek[0];

            for (int i = 1; i < daysOfWeek.Count; i++)
            {
                var idx = daysOfWeek[i];
                int sneek = 100;
                if (i < daysOfWeek.Count - 1)
                {
                    sneek = daysOfWeek[i + 1];
                }

                if (idx - startidx == 1 && sneek - idx == 1)
                {
                    start += "-";
                }
                else if (idx - startidx == 1 && sneek - idx > 1)
                {
                    start += "-" + DayNameHelper.ToShortestDayName(idx) + ",";
                }
                else
                {
                    start = start + "," + DayNameHelper.ToShortestDayName(idx);
                }

                startidx = idx;
            }

            while (start.Contains("--"))
            {
                start = start.Replace("--", "-");
            }

            while (start.Contains(",,"))
            {
                start = start.Replace(",,", ",");
            }

            start = start.Trim().Trim('-').Trim(',');

            return start;
        }

        private static List<int> GetDaysOfWeekList(this IDailyTimer channel)
        {
            var daysOfWeek = new List<int>();
            if (channel.Monday)
                daysOfWeek.Add(1);
            if (channel.Tuesday)
                daysOfWeek.Add(2);
            if (channel.Wednesday)
                daysOfWeek.Add(3);
            if (channel.Thursday)
                daysOfWeek.Add(4);
            if (channel.Friday)
                daysOfWeek.Add(5);
            if (channel.Saturday)
                daysOfWeek.Add(6);
            if (channel.Sunday)
                daysOfWeek.Add(7);
            return daysOfWeek;
        }

        public static int GetHour(this IDailyTimer channel)
        {
            if (int.TryParse(channel.SwitchTime.Split(':')[0].TrimStart('0'), out int result))
            {
                return result;
            }
            else
            {
                return 0;
            }
        }

        public static int GetMinute(this IDailyTimer channel)
        {
            if (int.TryParse(channel.SwitchTime.Split(':')[1].TrimStart('0'), out int result))
            {
                return result;
            }
            else
            {
                return 0;
            }
        }

        public static void CopyTo(this IDailyTimer channel, IDailyTimer copy)
        {
            copy.Name = channel.Name;
            copy.Active = channel.Active;
            copy.Description = channel.Description;
            copy.Monday = channel.Monday;
            copy.Tuesday = channel.Tuesday;
            copy.Wednesday = channel.Wednesday;
            copy.Thursday = channel.Thursday;
            copy.Friday = channel.Friday;
            copy.Saturday = channel.Saturday;
            copy.Sunday = channel.Sunday;
            copy.IgnoreOnHolidays = channel.IgnoreOnHolidays;
            copy.SwitchTime = channel.SwitchTime;
        }

        public static string GetDisplayText(this IDailyTimer channel)
        {
            var daysOfWeek = GetAbbreviationText(channel);

            return channel.Name + ": " + daysOfWeek + " " + channel.SwitchTime;
        }

        public static string GetDisplayHour(this IDailyTimer channel)
        {
            string hours = channel.SwitchTime?.Split(':')[0];
            if (string.IsNullOrEmpty(hours))
            {
                hours = "12";
            }
            return hours.PadLeft(2); ;
        }

        public static string GetDisplayMinute(this IDailyTimer channel)
        {
            var minutes = channel.SwitchTime?.Split(':')[1];
            if (string.IsNullOrEmpty(minutes))
            {
                minutes = "00";
            }
            return minutes.PadLeft(2); ;
        }

        public static IList<IDailyTimer> SortByName(this IList<IDailyTimer> channels)
        {
            var l = channels.ToList();
            l.Sort(IDailyTimerComparer.CompareByName);
            return l;
        }
    }
}