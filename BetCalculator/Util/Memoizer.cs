using System;

namespace BetCalculator.Util
{
    public static class Memoizer
    {
        public static Func<TR> Memoize<TR>(Func<TR> func)
        {
            object cache = null;
            return () =>
            {
                cache ??= func();
                return (TR) cache;
            };
        }
    }
}