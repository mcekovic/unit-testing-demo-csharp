using System;
using System.Collections.Generic;

namespace BetCalculator.BetTypes
{
    public record Accumulator : BetType
    {
        public override IEnumerator<IList<T>> Combinations<T>(IList<T> items)
        {
            yield return items;
        }
    }

    public record AccumulatorN : Accumulator
    {
        public int ItemCount { get; }
 
        public AccumulatorN(int itemCount)
        {
            if (itemCount < 1)
                throw new ArgumentException("itemCount must be at least 1");
            ItemCount = itemCount;
        }

        public override IEnumerator<IList<T>> Combinations<T>(IList<T> items)
        {
            if (items.Count != ItemCount)
                throw new ArgumentException("legs/groups size must be " + ItemCount);
            return base.Combinations(items);
        }
    }

    public record Single : AccumulatorN
    {
        public Single() : base(1) {}
    }
}
