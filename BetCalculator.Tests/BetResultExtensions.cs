using FluentAssertions;
using FluentAssertions.Primitives;

namespace BetCalculator.Tests
{
    public static class BetResultExtensions
    {
        public static BetResultAssertions Should(this BetResult instance) => new(instance);

        public class BetResultAssertions : ReferenceTypeAssertions<BetResult, BetResultAssertions>
        {
            public BetResultAssertions(BetResult instance) => Subject = instance;

            protected override string Identifier => "result";

            public AndConstraint<BetResultAssertions> HaveUnitCount(decimal unitCount)
            {
                Subject.UnitCount.Should().Be(unitCount);
                return new AndConstraint<BetResultAssertions>(this);
            }

            public AndConstraint<BetResultAssertions> HaveStake(decimal stake)
            {
                Subject.Stake.Should().Be(stake);
                return new AndConstraint<BetResultAssertions>(this);
            }

            public AndConstraint<BetResultAssertions> HaveMaxReturn(decimal maxReturn)
            {
                Subject.MaxReturn.Should().Be(maxReturn);
                return new AndConstraint<BetResultAssertions>(this);
            }
        }
    }
}