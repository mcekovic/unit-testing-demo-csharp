using System;
using System.Collections.Generic;
using System.Linq;
using BetCalculator.BetTypes;
using BetCalculator.Rules;
using BetCalculator.Util;
using static BetCalculator.BetState;
using static BetCalculator.Rules.EachWayType;

namespace BetCalculator
{
    public class BetUnit
    {
        public decimal UnitStake { get; }
        public IList<BetLeg> Legs { get; }
        public BetType BetType { get; }
        public BetRules Rules { get; }

        private readonly Func<decimal> _stakeFunc;
        private readonly Func<decimal> _cumulativePriceFunc;
        private readonly Func<BetState> _stateFunc;

        public BetUnit(decimal unitStake, IList<BetLeg> legs, BetType betType, BetRules rules)
        {
            UnitStake = unitStake;
            Legs = legs;
            BetType = betType;
            Rules = rules;
            _stakeFunc = Memoizer.Memoize(StakeFunc);
            _cumulativePriceFunc = Memoizer.Memoize(CumulativePriceFunc);
            _stateFunc = Memoizer.Memoize(StateFunc);
        }

        public decimal UnitCount() => UnitCount(Rules.EachWayType);

        private static decimal UnitCount(EachWayType eachWayType) =>
            eachWayType switch
            {
                Win or Place => 1m,
                EachWay => 2m
            };
        
        public decimal Stake() => _stakeFunc.Invoke();

        private decimal StakeFunc() => UnitCount() * UnitStake;

        public decimal CurrentReturn()
        {
            var state = State();
            return state == Won || state == BetState.Void ? Return() : 0m;
        }

        public decimal MaxReturn() => State() != Lost ? Return() : 0m;

        private decimal Return() => Stake() * CumulativePrice();

        public decimal CumulativePrice() => _cumulativePriceFunc.Invoke();
        
        private decimal CumulativePriceFunc() =>
            Rules.EachWayType != EachWay ? SimpleCumulativePrice() : EachWayCumulativePrice();

        private decimal SimpleCumulativePrice() =>
            Legs.Aggregate(1m, (current, leg)
                => current * leg.FactoredPrice(Rules.EachWayType)
            );

        private decimal EachWayCumulativePrice() =>
            Legs.Aggregate(EachWayAmounts.One, (current, leg)
                => current * new EachWayAmounts(leg.FactoredPrice(Win), leg.FactoredPrice(Place))
            ).Total / UnitCount(Rules.EachWayType);
        
        public BetState State() => _stateFunc.Invoke();

        private BetState StateFunc() =>
            Legs.Aggregate(BetState.Void, (state, leg) => BetStates.LegAggregate(state, leg.Status.State));
    }
}
