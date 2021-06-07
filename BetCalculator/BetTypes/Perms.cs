using System;
using System.Collections.Generic;
using BetCalculator.Util;

namespace BetCalculator.BetTypes
{
    public record Perms : BetType 
    {
        public int CombinationSize { get; }

        public Perms(int combinationSize) : this(combinationSize, true) {}
        
        protected Perms(int combinationSize, bool canSkipUnits) : base(canSkipUnits)
        {
            if (combinationSize < 1)
                throw new ArgumentException("combinationSize must be at least 1");
            CombinationSize = combinationSize;
        }

        public override IEnumerator<IList<T>> Combinations<T>(IList<T> items)
        {
            if (items.Count < CombinationSize)
                throw new ArgumentException("legs/groups size must be at least " + CombinationSize);
            return new Combinations<T>(items, CombinationSize);
        }
    }

    public record StrictPerms : Perms
    {
        public StrictPerms(int combinationSize) : base(combinationSize, false) {}
    }
}
