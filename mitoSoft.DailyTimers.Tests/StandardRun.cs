using Microsoft.VisualStudio.TestTools.UnitTesting;
using mitoSoft.DailyTimers.Core.Contracts;
using mitoSoft.DailyTimers.Core.Helpers;

namespace mitoSoft.DailyTimers.Tests
{
    [TestClass]
    public class StandardRun
    {
        [TestMethod]
        public void Run()
        {
            var timer = new Timer();

            var checker = new Checker(timer, Holidays.Provinces.RheinlandPfalz);

            bool result;
            result = checker.CheckChannelState(new System.DateTime(2021, 1, 1, 11, 55, 0));

            Assert.IsFalse(result);

            //simulate overrun
            result = checker.CheckChannelState(new System.DateTime(2021, 1, 1, 12, 1, 0));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestIgnoreHolidays()
        {
            var timer = new Timer
            {
                IgnoreOnHolidays = true, //do not raise on holidays
            };

            var checker = new Checker(timer, Holidays.Provinces.RheinlandPfalz);

            bool result;
            result = checker.CheckChannelState(new System.DateTime(2021, 1, 1, 11, 55, 0));

            Assert.IsFalse(result);

            //simulate overrun
            result = checker.CheckChannelState(new System.DateTime(2021, 1, 1, 12, 1, 0));

            Assert.IsFalse(result);
        }

        private class Timer : IDailyTimer
        {
            public bool Active { get; set; } = true;
            public string Name { get; set; } = "Test";
            public string Description { get; set; } = "Test";
            public bool Monday { get; set; } = true;
            public bool Tuesday { get; set; } = true;
            public bool Wednesday { get; set; } = true;
            public bool Thursday { get; set; } = true;
            public bool Friday { get; set; } = true;
            public bool Saturday { get; set; } = true;
            public bool Sunday { get; set; } = true;
            public string SwitchTime { get; set; } = "12:00";
            public bool IgnoreOnHolidays { get; set; } = false;

            public object Clone()
            {
                throw new System.NotImplementedException();
            }
        }
    }
}
