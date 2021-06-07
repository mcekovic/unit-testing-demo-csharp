using FluentAssertions;
using Xunit;
using static BetCalculator.BetState;
using static BetCalculator.BetStates;

namespace BetCalculator.Tests
{
    public class BetStateTest
    {
        [Fact]
        public void UnitAggregateTest()
        {
            UnitAggregate(Open, Open).Should().Be(Open);
            UnitAggregate(Open, Won).Should().Be(Open);
            UnitAggregate(Open, Void).Should().Be(Open);
            UnitAggregate(Open, Lost).Should().Be(Open);

            UnitAggregate(Won, Open).Should().Be(Open);
            UnitAggregate(Won, Won).Should().Be(Won);
            UnitAggregate(Won, Void).Should().Be(Won);
            UnitAggregate(Won, Lost).Should().Be(Won);

            UnitAggregate(Void, Open).Should().Be(Open);
            UnitAggregate(Void, Won).Should().Be(Won);
            UnitAggregate(Void, Void).Should().Be(Void);
            UnitAggregate(Void, Lost).Should().Be(Void);

            UnitAggregate(Lost, Open).Should().Be(Open);
            UnitAggregate(Lost, Won).Should().Be(Won);
            UnitAggregate(Lost, Void).Should().Be(Void);
            UnitAggregate(Lost, Lost).Should().Be(Lost);
        }

        [Fact]
        public void LegAggregateTest() {
            LegAggregate(Open, Open).Should().Be(Open);
            LegAggregate(Open, Won).Should().Be(Open);
            LegAggregate(Open, Void).Should().Be(Open);
            LegAggregate(Open, Lost).Should().Be(Lost);

            LegAggregate(Won, Open).Should().Be(Open);
            LegAggregate(Won, Won).Should().Be(Won);
            LegAggregate(Won, Void).Should().Be(Won);
            LegAggregate(Won, Lost).Should().Be(Lost);

            LegAggregate(Void, Open).Should().Be(Open);
            LegAggregate(Void, Won).Should().Be(Won);
            LegAggregate(Void, Void).Should().Be(Void);
            LegAggregate(Void, Lost).Should().Be(Lost);

            LegAggregate(Lost, Open).Should().Be(Lost);
            LegAggregate(Lost, Won).Should().Be(Lost);
            LegAggregate(Lost, Void).Should().Be(Lost);
            LegAggregate(Lost, Lost).Should().Be(Lost);
        }
    }
}