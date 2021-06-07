using BetCalculator.Interrelation;
using FluentAssertions;
using Xunit;

namespace BetCalculator.Tests.Interrelation
{
    public class IrDetectorTest
    {
        [Fact]
        public void SameSelectionInterrelation()
        {
            var desc1 = new IrDescriptor(111, 11, 1);
            var desc2 = new IrDescriptor(111, 11, 1);

            IrDetector.AreInterrelated(desc1, desc2).IrType.Should().Be(IrType.SameSelection);
        }
        
        [Fact]
        public void SameSingleWinnerMarketInterrelation()
        {
            var desc1 = new IrDescriptor(111, 11, 1);
            var desc2 = new IrDescriptor(112, 11, 1);

            IrDetector.AreInterrelated(desc1, desc2).IrType.Should().Be(IrType.SameMarket);
        }
        
        [Fact]
        public void SameMultiWinnerMarketNoInterrelation()
        {
            var desc1 = new IrDescriptor(111, 11, 1, null);
            var desc2 = new IrDescriptor(112, 11, 1, null);

            IrDetector.AreInterrelated(desc1, desc2).IsInterrelated().Should().Be(false);
        }
    }
}