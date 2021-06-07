using System;
using System.Collections.Generic;
using System.Linq;
using BetCalculator.Util;

namespace BetCalculator.BetTypes
{
    public record FullCover : BetType
    {
    
        public static readonly FullCover Trixie = new FullCoverN(3, false);
        public static readonly FullCover Patent = new FullCoverN(3, true);
        public bool withSingles { get; }

        private readonly int _fromCombinations;

        public FullCover(bool withSingles = false)
        {
            this.withSingles = withSingles;
            _fromCombinations = withSingles ? 1 : 2;
        }

        public override IEnumerator<IList<T>> Combinations<T>(IList<T> items)
        {
            if (items.Count < _fromCombinations)
                throw new ArgumentException("legs/groups size must be at least " + _fromCombinations);
            return new CompositeEnumerator<IList<T>>(
                Enumerable.Range(_fromCombinations, items.Count - _fromCombinations + 1)
                    .Select(count => new Combinations<T>(items, count))
            );
        }
    }
    
    public record FullCoverN : FullCover
    {
        public int ItemCount { get; }
 
        public FullCoverN(int itemCount, bool withSingles = false) : base(withSingles)
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
}
