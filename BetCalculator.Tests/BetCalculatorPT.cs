using System.Collections.Generic;
using BetCalculator.BetTypes;
using Xunit;

namespace BetCalculator.Tests
{
    public class BetCalculatorPT
    {
        [Fact]
        public void TestBigPerms()
        {
            var bet = new Bet { BetType = new FullCoverN(22), UnitStake = 1m, Legs = MakeLegs(22) };

            var result = bet.Calculate();

            result.Should().HaveUnitCount(4194281m).And.HaveStake(4194281m);
        } 
    
        private static IList<BetLeg> MakeLegs(int count) {
            var legs = new List<BetLeg>(count);
            for (var i = 0; i < count; i++)
                legs.Add(new BetLeg { Price = 1m + i });
            return legs;
        }
    }
}
