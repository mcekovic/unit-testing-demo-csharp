using System.Collections.Generic;
using BetCalculator.Util;
using FluentAssertions;
using Xunit;

namespace BetCalculator.Tests.Util
{
    public class CompositeEnumeratorTest
    {
        [Fact]
        public void NoEnumerators()
        {
            var enumerator = new CompositeEnumerator<int>(System.Array.Empty<IEnumerator<int>>());
            enumerator.MoveNext().Should().BeFalse();
        }
        
        [Fact]
        public void OneEnumerator()
        {
            var enumerator = new CompositeEnumerator<int>(new List<IEnumerator<int>> {
                new List<int> { 1, 2 }.GetEnumerator(),
                new List<int> { 3 }.GetEnumerator()
            });
            enumerator.MoveNext().Should().BeTrue();
            enumerator.Current.Should().Be(1);
            enumerator.MoveNext().Should().BeTrue();
            enumerator.Current.Should().Be(2);
            enumerator.MoveNext().Should().BeTrue();
            enumerator.Current.Should().Be(3);
            enumerator.MoveNext().Should().BeFalse();
        }
        
        [Fact]
        public void ManyEnumerators()
        {
            var enumerator = new CompositeEnumerator<int>(new List<IEnumerator<int>> {
                new List<int> { 1 }.GetEnumerator(),
                new List<int> { 2 }.GetEnumerator(),
                new List<int> { 3 }.GetEnumerator()
            });
            enumerator.MoveNext().Should().BeTrue();
            enumerator.Current.Should().Be(1);
            enumerator.MoveNext().Should().BeTrue();
            enumerator.Current.Should().Be(2);
            enumerator.MoveNext().Should().BeTrue();
            enumerator.Current.Should().Be(3);
            enumerator.MoveNext().Should().BeFalse();
        }
    }
}