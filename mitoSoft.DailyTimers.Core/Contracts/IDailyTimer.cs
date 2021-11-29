using System;

namespace mitoSoft.DailyTimers.Core.Contracts
{
    public interface IDailyTimer : ICloneable
    {
        bool Active { get; set; }

        string Name { get; set; }

        string Description { get; set; }

        bool Monday { get; set; }

        bool Tuesday { get; set; }

        bool Wednesday { get; set; }

        bool Thursday { get; set; }

        bool Friday { get; set; }

        bool Saturday { get; set; }

        bool Sunday { get; set; }

        string SwitchTime { get; set; }

        bool IgnoreOnHolidays { get; set; }
    }
}