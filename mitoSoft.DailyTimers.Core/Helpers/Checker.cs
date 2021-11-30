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

        public IDailyTimer Timer { get; private set; }

        public Checker(IDailyTimer channel, Provinces state) : this(channel, state, DateTime.Now)
        {
        }

        public Checker(IDailyTimer channel, Provinces state, DateTime now)
        {
            _lastRun = now;
            _state = state;

            Timer = channel;
        }

        public bool CheckChannelState()
        {
            return CheckChannelState(DateTime.Now);
        }

        public bool CheckChannelState(DateTime now)
        {
            if (!Timer.Active)
            {
                _lastRun = now;
                return false;
            }

            if (Timer.IgnoreOnHolidays && now.IsHoliday(_state)) //do not trigger on holidays
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

        private bool IsTimeOverrun(DateTime now)
        {
            var trigger = new DateTime(now.Year, now.Month, now.Day, Timer.GetHour(), Timer.GetMinute(), 0);
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
            if (now.DayOfWeek == DayOfWeek.Monday && Timer.Monday)
            {
                return true;
            }
            else if (now.DayOfWeek == DayOfWeek.Tuesday && Timer.Tuesday)
            {
                return true;
            }
            else if (now.DayOfWeek == DayOfWeek.Wednesday && Timer.Wednesday)
            {
                return true;
            }
            else if (now.DayOfWeek == DayOfWeek.Thursday && Timer.Thursday)
            {
                return true;
            }
            else if (now.DayOfWeek == DayOfWeek.Friday && Timer.Friday)
            {
                return true;
            }
            else if (now.DayOfWeek == DayOfWeek.Saturday && Timer.Saturday)
            {
                return true;
            }
            else if (now.DayOfWeek == DayOfWeek.Sunday && Timer.Sunday)
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