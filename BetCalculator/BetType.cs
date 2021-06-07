using System.Collections.Generic;

namespace BetCalculator
{
    public abstract record BetType
    {
        public bool CanSkipUnits { get; }

        protected BetType(bool canSkipUnits = false)
        {
            CanSkipUnits = canSkipUnits;
        }

        public abstract IEnumerator<IList<T>> Combinations<T>(IList<T> items);
    }
}
