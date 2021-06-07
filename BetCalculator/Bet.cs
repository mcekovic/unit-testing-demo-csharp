using System.Collections.Generic;
using BetCalculator.Interrelation;
using BetCalculator.Rules;

namespace BetCalculator
{
    public class Bet
    {
        public BetType BetType { get; init; }
        public decimal UnitStake { get; init; }
        public IList<BetLeg> Legs { get; init; }
        public BetRules Rules { get; init; } = BetRules.Default;
        
        public BetResult Calculate()
        {
            var result = BetResult.Empty;
            var irDetector = new IrDetector();
            var combinations = BetType.Combinations(Legs);
            while (combinations.MoveNext())
            {
                var unit = new BetUnit(UnitStake, combinations.Current, BetType, Rules);
                if (irDetector.CheckInterrelationOrSkip(unit))
                    continue;
                result += new BetResult(unit.UnitCount(), unit.Stake(), unit.MaxReturn());
            }
            if (result == BetResult.Empty)
                throw new IrException(IrType.NoValidUnits, "No valid units");
            return result;
        }
    }
}
