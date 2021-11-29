using mitoSoft.DailyTimers.Core.Contracts;

namespace mitoSoft.DailyTimers.Core.Comparer
{
    internal static class IDailyTimerComparer
    {
        public static int CompareByName(IDailyTimer x, IDailyTimer y)
        {
            return x.Name.CompareTo(y.Name);
        }
    }
}