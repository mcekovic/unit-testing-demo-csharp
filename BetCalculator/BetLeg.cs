using BetCalculator.Interrelation;
using BetCalculator.Rules;

namespace BetCalculator
{
    public class BetLeg
    {
        public decimal Price { get; init; }
        public LegStatus Status { get; init; } = LegStatus.Open;
        public IrDescriptor IrDescriptor { get; init; } = IrDescriptor.NoIr();
        
        public decimal FactoredPrice(EachWayType eachWayType) =>
            Status.FactoredPrice(Price, eachWayType);
    }
}