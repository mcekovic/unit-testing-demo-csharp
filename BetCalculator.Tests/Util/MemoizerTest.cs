using System;
using BetCalculator.Util;
using FluentAssertions;
using Xunit;
using Moq;

namespace BetCalculator.Tests.Util
{
    public class MemoizerTest
    {
        [Fact]
        public void Memoize()
        {
            var funcMock = new Mock<Func<int>>();
            funcMock.Setup(func => func.Invoke()).Returns(5);
            var memoFunc = Memoizer.Memoize(funcMock.Object);
            
            funcMock.Verify(func => func.Invoke(), Times.Never);

            memoFunc.Invoke().Should().Be(5);
            funcMock.Verify(func => func.Invoke(), Times.Exactly(1));

            memoFunc.Invoke();
            memoFunc.Invoke();
            funcMock.Verify(func => func.Invoke(), Times.Exactly(1));
        }
    }
}