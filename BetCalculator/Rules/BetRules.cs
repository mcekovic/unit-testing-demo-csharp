namespace BetCalculator.Rules
{
    public readonly struct BetRules
    {
        public static readonly BetRules Default = new() { EachWayType = EachWayType.Win };
        
        public EachWayType EachWayType { get; init; }
    }
}