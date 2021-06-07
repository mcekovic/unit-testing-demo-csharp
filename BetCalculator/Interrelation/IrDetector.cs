using System;
using System.Collections.Generic;
using System.Linq;

namespace BetCalculator.Interrelation
{
    public class IrDetector
    {
        private readonly IDictionary<IrKey, IrResult> _irCache = new Dictionary<IrKey, IrResult>();

        public bool CheckInterrelationOrSkip(BetUnit unit)
        {
            var result = AreInterrelated(unit.Legs);
            if (!result.IsInterrelated())
                return false;
            if (!unit.BetType.CanSkipUnits)
                throw result.ToIrException();
            return true;
        }

        private IrResult AreInterrelated(IList<BetLeg> legs)
        {
            var count = legs.Count;
            for (var i = 0; i < count - 1; i++)
            {
                var desc1 = legs[i].IrDescriptor;
                for (var j = i + 1; j < count; j++)
                {
                    var desc2 = legs[j].IrDescriptor;
                    var result = CachedAreInterrelated(desc1, desc2);
                    if (result.IsInterrelated())
                        return result;
                }
            }
            return IsMaxWinnersViolated(legs);
        }

        private IrResult CachedAreInterrelated(IrDescriptor desc1, IrDescriptor desc2)
        {
            var irKey = new IrKey(desc1.SelectionId, desc2.SelectionId);
            if (!_irCache.TryGetValue(irKey, out var result))
            {
                result = AreInterrelated(desc1, desc2);
                _irCache[irKey] = result;
            }
            return result;
        }

        private readonly struct IrKey
        {
            private readonly object _selectionId1;
            private readonly object _selectionId2;

            public IrKey(object selectionId1, object selectionId2)
            {
                _selectionId1 = selectionId1;
                _selectionId2 = selectionId2;
            }

            public override bool Equals(object obj) => obj is IrKey other && _selectionId1.Equals(other._selectionId1) && _selectionId2.Equals(other._selectionId2);
            
            public override int GetHashCode() => 31 * _selectionId1.GetHashCode() + _selectionId2.GetHashCode();
        }

        public static IrResult AreInterrelated(IrDescriptor desc1, IrDescriptor desc2)
        {
            if (Equals(desc1.SelectionId, desc2.SelectionId))
                return new IrSelectionsResult(IrType.SameSelection, desc1.SelectionId, desc2.SelectionId, $"Same Selection ({desc1.SelectionId})");
            if (Equals(desc1.MarketId, desc2.MarketId))
            {
                if (!Equals(desc1.MaxWinners, desc2.MaxWinners))
                    throw new DataMisalignedException("Different MaxWinners for different Legs on the same Market");
                if (desc1.MaxWinners == 1)
                    return new IrSelectionsResult(IrType.SameMarket, desc1.SelectionId, desc2.SelectionId, $"Same Market ({desc1.MarketId})");
                return IrResult.NotInterrelated;
            }
            return IrResult.NotInterrelated;
        }

        private static IrResult IsMaxWinnersViolated(IList<BetLeg> legs)
        {
            IDictionary<object, int?> marketCounts = null;
            foreach (var leg in legs)
            {
                var desc = leg.IrDescriptor;
                var marketId = desc.MarketId;
                var maxWinners = desc.MaxWinners;
                if (maxWinners == null || maxWinners == 1)
                    continue;
                marketCounts ??= new Dictionary<object, int?>();
                if (marketCounts.TryGetValue(marketId, out var marketCount))
                    marketCount++;
                else
                    marketCount = 1;
                if (marketCount > maxWinners)
                    return new MaxWinnersViolationIrResult(marketId, FindSelectionIdsForMarket(legs, marketId), (int) maxWinners);
                marketCounts[marketId] = marketCount;
            }
            return IrResult.NotInterrelated;
        }

        private static IEnumerable<object> FindSelectionIdsForMarket(IEnumerable<BetLeg> legs, object marketId) =>
            legs.Where(leg => leg.IrDescriptor.MarketId == marketId);
    }
}