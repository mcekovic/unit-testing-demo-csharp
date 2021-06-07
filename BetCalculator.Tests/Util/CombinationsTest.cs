using System;
using System.Collections.Generic;
using BetCalculator.Util;
using FluentAssertions;
using Xunit;

namespace BetCalculator.Tests.Util
{
    public class CombinationsTest
    {
        [Fact]
        public void Combinations() {
            var combinations = new Combinations<string>(new List<string> {"A", "B", "C"}, 2);
            combinations.MoveNext().Should().BeTrue();
            combinations.Current.Should().ContainInOrder("A", "B");
            combinations.MoveNext().Should().BeTrue();
            combinations.Current.Should().ContainInOrder("A", "C");
            combinations.MoveNext().Should().BeTrue();
            combinations.Current.Should().ContainInOrder("B", "C");
            combinations.MoveNext().Should().BeFalse();
        }

        [Fact]
        public void EmptyItems() {
            new Combinations<string>(new List<string>(), 0).MoveNext().Should().BeFalse();
        }

        [Fact]
        public void InvalidCombinations()
        {
            Action act1 = () => new Combinations<string>(new List<string> {"A", "B"}, -1);
            act1.Should().Throw<ArgumentException>();
            Action act2 = () => new Combinations<string>(new List<string> {"A", "B"}, 3);
            act2.Should().Throw<ArgumentException>();
        }
    }
}
