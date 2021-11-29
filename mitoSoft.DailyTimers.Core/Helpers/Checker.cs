using mitoSoft.DailyTimers.Core.Contracts;
using mitoSoft.DailyTimers.Core.Extensions;
using mitoSoft.Holidays;
using mitoSoft.Holidays.Extensions;
using System;

namespace mitoSoft.DailyTimers.Core.Helpers
{
    public class Checker
    {
        private DateTime _lastRun;
        private readonly Provinces _state;

        public IDailyTimer Channel { get; private set; }

        public Checker(IDailyTimer channel, Provinces state) : this(channel, state, DateTime.Now)
        {
        }

        public Checker(IDailyTimer channel, Provinces state, DateTime now)
        {
            _lastRun = now;
            _state = state;

            Channel = channel;
        }

        public bool CheckChannelState()
        {
            return CheckChannelState(DateTime.Now);
        }

        public bool CheckChannelState(DateTime now)
        {
            if (Channel.IgnoreOnHolidays && now.IsHoliday(_state))
            {
                _lastRun = now;
                return false;
            }

            if (!IsDayOfWeek(now))
            {
                _lastRun = now;
                return false;
            }

            var result = IsTimeOverrun(now);

            _lastRun = now;
            return result;
        }

        //Beispiel
        //_lastRun = 9:50
        //trigger = 10:00
        //now = 10:05
        //-> true
        private bool IsTimeOverrun(DateTime now)
        {
            var trigger = new DateTime(now.Year, now.Month, now.Day, Channel.GetHour(), Channel.GetMinute(), 0);
            if (_lastRun < trigger && trigger <= now)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsDayOfWeek(DateTime now)
        {
            if (now.DayOfWeek == DayOfWeek.Monday && Channel.Monday)
            {
                return true;
            }
            else if (now.DayOfWeek == DayOfWeek.Tuesday && Channel.Tuesday)
            {
                return true;
            }
            else if (now.DayOfWeek == DayOfWeek.Wednesday && Channel.Wednesday)
            {
                return true;
            }
            else if (now.DayOfWeek == DayOfWeek.Thursday && Channel.Thursday)
            {
                return true;
            }
            else if (now.DayOfWeek == DayOfWeek.Friday && Channel.Friday)
            {
                return true;
            }
            else if (now.DayOfWeek == DayOfWeek.Saturday && Channel.Saturday)
            {
                return true;
            }
            else if (now.DayOfWeek == DayOfWeek.Sunday && Channel.Sunday)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}