using BetCalculator.Rules;
using FluentAssertions;
using Xunit;

namespace BetCalculator.Tests.Rules
{
    public class EachWayAmountsTest
    {
        [Fact]
        public void PlusTest()
        {
            (new EachWayAmounts(1m, 2m) + new EachWayAmounts(3m, 4m))
                .Should().Be(new EachWayAmounts(4m, 6m));
        }

        [Fact]
        public void TimesTest()
        {
            (new EachWayAmounts(1m, 2m) * new EachWayAmounts(3m, 4m))
                .Should().Be(new EachWayAmounts(3m, 8m));
        }

        [Fact]
        public void TotalTest()
        {
            new EachWayAmounts(4m, 2m).Total.Should().Be(6m);
            EachWayAmounts.Zero.Total.Should().Be(0m);
        }
    }
}