using System;
using static BetCalculator.BetState;

namespace BetCalculator
{
    public enum BetState { Open, Won, Void, Lost }

    public static class BetStates
    {
        public static BetState LegAggregate(BetState state1, BetState state2) =>
            state1 == Lost || state2 == Lost ? Lost : MinBetState(state1, state2);

        public static BetState UnitAggregate(BetState state1, BetState state2) =>
            MinBetState(state1, state2);

        private static BetState MinBetState(BetState state1, BetState state2) =>
            (BetState) Math.Min((ulong) state1, (ulong) state2);
    }
}