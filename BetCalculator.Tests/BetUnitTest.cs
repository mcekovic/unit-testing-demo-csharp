using System.Collections.Generic;
using BetCalculator.BetTypes;
using BetCalculator.Rules;
using FluentAssertions;
using Xunit;

namespace BetCalculator.Tests
{
    public class BetUnitTest
    {
        [Fact]
        void OpenUnitTest() {
            var unit = new BetUnit(10m, new List<BetLeg> {
                new() { Price = 2m, Status = LegStatus.Won },
                new() { Price = 3m, Status = LegStatus.Open }
            }, new Accumulator(), BetRules.Default);

            unit.MaxReturn().Should().Be(60m);
            unit.State().Should().Be(BetState.Open);
        }

        [Fact]
        void WonUnitTest() {
            var unit = new BetUnit(10m, new List<BetLeg> {
                new() { Price = 2m, Status = LegStatus.Won },
                new() { Price = 3m, Status = LegStatus.Void }
            }, new Accumulator(), BetRules.Default);

            unit.MaxReturn().Should().Be(20m);
            unit.State().Should().Be(BetState.Won);
        }

        [Fact]
        void VoidUnitTest() {
            var unit = new BetUnit(10m, new List<BetLeg> {
                new() { Price = 2m, Status = LegStatus.Void },
                new() { Price = 3m, Status = LegStatus.Void }
            }, new Accumulator(), BetRules.Default);

            unit.MaxReturn().Should().Be(10m);
            unit.State().Should().Be(BetState.Void);
        }

        [Fact]
        void LostUnitTest() {
            var unit = new BetUnit(10m, new List<BetLeg> {
                new() { Price = 2m, Status = LegStatus.Open },
                new() { Price = 3m, Status = LegStatus.Lost }
            }, new Accumulator(), BetRules.Default);

            unit.MaxReturn().Should().Be(0m);
            unit.State().Should().Be(BetState.Lost);
        }
    }
}