using System;
using System.Collections.Generic;
using BetCalculator.BetTypes;
using BetCalculator.Interrelation;
using FluentAssertions;
using Xunit;

namespace BetCalculator.Tests.Interrelation
{
    public class InterrelationDetectionTest
    {
        [Fact]
        public void InterrelatedSelectionsAreDetected() {
            var bet = new Bet { BetType = new Accumulator(), UnitStake = 1m, Legs = new List<BetLeg> {
                new () { Price = 2m, IrDescriptor = new IrDescriptor(111, 11, 1) },
                new () { Price = 3m, IrDescriptor = new IrDescriptor(112, 11, 1) }
            }};

            Action act = () => bet.Calculate();
            act.Should().Throw<IrSelectionsException>().And.IrType.Should().Be(IrType.SameMarket);
        }

        [Fact]
        public void MaxWinnersViolationIsDetected() {
            var bet = new Bet { BetType = new Accumulator(), UnitStake = 1m, Legs = new List<BetLeg> {
                new () { Price = 2m, IrDescriptor = new IrDescriptor(111, 11, 1, 2) },
                new () { Price = 3m, IrDescriptor = new IrDescriptor(112, 11, 1, 2) },
                new () { Price = 4m, IrDescriptor = new IrDescriptor(113, 11, 1, 2) }
            }};

            Action act = () => bet.Calculate();
            act.Should().Throw<MaxWinnersViolationException>();
        }
        
        [Fact]
        public void NoValidUnitsAreDetected() {
            var bet = new Bet { BetType = new Perms(2), UnitStake = 1m, Legs = new List<BetLeg> {
                new () { Price = 2m, IrDescriptor = new IrDescriptor(111, 11, 1) },
                new () { Price = 3m, IrDescriptor = new IrDescriptor(112, 11, 1) },
                new () { Price = 4m, IrDescriptor = new IrDescriptor(113, 11, 1) }
            }};

            Action act = () => bet.Calculate();
            act.Should().Throw<IrException>().And.IrType.Should().Be(IrType.NoValidUnits);
        }
    }
}