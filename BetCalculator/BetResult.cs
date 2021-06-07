namespace BetCalculator
{
    public record BetResult
    {
        public static readonly BetResult Empty = new(0m, 0m, 0m);

        public decimal UnitCount { get; }
        public decimal Stake { get; }
        public decimal MaxReturn { get; }

        public BetResult(decimal unitCount, decimal stake, decimal maxReturn)
        {
            UnitCount = unitCount;
            Stake = stake;
            MaxReturn = maxReturn;
        }

        public static BetResult operator +(BetResult r1, BetResult r2) =>
            new(r1.UnitCount + r2.UnitCount, r1.Stake + r2.Stake, r1.MaxReturn + r2.MaxReturn);
    }
}