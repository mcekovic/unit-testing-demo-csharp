namespace BetCalculator.Interrelation
{
    public record IrDescriptor
    {
        public static IrDescriptor NoIr() => new(maxWinners: null);
 
        public object SelectionId { get; } 
        public object MarketId { get; } 
        public object EventId { get; }
        public int? MaxWinners { get; }
        
        public IrDescriptor(object selectionId = null, object marketId = null, object eventId = null, int? maxWinners = 1)
        {
            SelectionId = selectionId ?? new object();
            MarketId = marketId ?? new object();
            EventId = eventId ?? new object();
            MaxWinners = maxWinners;
        }
    }
}