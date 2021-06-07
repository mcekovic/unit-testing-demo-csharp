using System;
using System.Collections.Generic;
using BetCalculator.BetTypes;
using BetCalculator.Interrelation;
using BetCalculator.Rules;
using FluentAssertions;
using Moq;
using Xunit;
using Single = BetCalculator.BetTypes.Single;

namespace BetCalculator.Tests
{
    public class BetCalculatorTest
    {
        [Fact]
        public void TestSingle()
        {
            var bet = new Bet { BetType = new Single(), UnitStake = 10m, Legs = new List<BetLeg> {
                new() { Price = 2m }
            }};

            var result = bet.Calculate();

            result.Should().HaveUnitCount(1m).And.HaveStake(10m).And.HaveMaxReturn(20m);
        }
        
        [Fact]
        public void TestSingle_Mockist()
        {
            var betLegs = new List<BetLeg> {
                new() { Price = 2m }
            };
            var betTypeMock = new Mock<BetType>(false);
            betTypeMock.Setup(betType => betType.Combinations(It.IsAny<IList<BetLeg>>()))
                .Returns(new List<List<BetLeg>> { betLegs }.GetEnumerator());
            var bet = new Bet { BetType = betTypeMock.Object, UnitStake = 10m, Legs = betLegs};

            var result = bet.Calculate();

            result.Should().HaveUnitCount(1m).And.HaveStake(10m).And.HaveMaxReturn(20m);
        }

        [Fact]
        public void TestAccumulator()
        {
            var bet = new Bet { BetType = new Accumulator(), UnitStake = 5m, Legs = new List<BetLeg> {
                new() { Price = 2m },
                new() { Price = 3m }
            }};

            var result = bet.Calculate();

            result.Should().HaveUnitCount(1m).And.HaveStake(5m).And.HaveMaxReturn(30m);
        }

        [Fact]
        public void TestPerms()
        {
            var bet = new Bet { BetType = new Perms(2), UnitStake = 1m, Legs = new List<BetLeg> {
                new () { Price = 2m },
                new () { Price = 3m },
                new () { Price = 4m }
            }};

            var result = bet.Calculate();

            result.Should().HaveUnitCount(3m).And.HaveStake(3m).And.HaveMaxReturn(26m);
        }

        [Fact]
        public void TestPatent()
        {
            var bet = new Bet { BetType = FullCover.Patent, UnitStake = 1m, Legs = new List<BetLeg> {
                new () { Price = 2m },
                new () { Price = 3m },
                new () { Price = 4m }
            }};

            var result = bet.Calculate();

            result.Should().HaveUnitCount(7m).And.HaveStake(7m).And.HaveMaxReturn(59m);
        }
        
        
        // Each-Way
        
        [Fact]
        public void TestEachWaySingleBet() {
            var bet = new Bet { BetType = new Single(), UnitStake = 10m, Legs = new List<BetLeg> {
                new () { Price = 2m, Status = LegStatus.OpenEw(0.5m)}
            }, Rules = new() { EachWayType = EachWayType.EachWay}};

            var result = bet.Calculate();

            result.Should().HaveUnitCount(2m).And.HaveStake(20m).And.HaveMaxReturn(35m);
        }
        
        [Fact]
        public void TestPlaceSingleBet() {
            var bet = new Bet { BetType = new Single(), UnitStake = 10m, Legs = new List<BetLeg> {
                new () { Price = 2m, Status = LegStatus.OpenEw(0.5m)}
            }, Rules = new() { EachWayType = EachWayType.Place}};

            var result = bet.Calculate();

            result.Should().HaveUnitCount(1m).And.HaveStake(10m).And.HaveMaxReturn(15m);
        }

        
        // Interrelation
        
        [Fact]
        public void TestPermsSkippingUnits()
        {
            var bet = new Bet { BetType = new Perms(2), UnitStake = 1m, Legs = new List<BetLeg> {
                new () { Price = 2m, IrDescriptor = new IrDescriptor(111, 11, 1) },
                new () { Price = 3m, IrDescriptor = new IrDescriptor(112, 11, 1) },
                new () { Price = 4m, IrDescriptor = new IrDescriptor(211, 21, 2) }
            }};

            var result = bet.Calculate();

            result.Should().HaveUnitCount(2m).And.HaveStake(2m).And.HaveMaxReturn(20m);
        }
        
        [Fact]
        public void TestPatentFailsOnInterrelatedSelections()
        {
            var bet = new Bet { BetType = FullCover.Patent, UnitStake = 1m, Legs = new List<BetLeg> {
                new () { Price = 2m, IrDescriptor = new IrDescriptor(111, 11, 1) },
                new () { Price = 3m, IrDescriptor = new IrDescriptor(112, 11, 1) },
                new () { Price = 4m, IrDescriptor = new IrDescriptor(211, 21, 2) }
            }};

            Action act = () => bet.Calculate();

            act.Should().Throw<IrException>();
        }
    }
}
