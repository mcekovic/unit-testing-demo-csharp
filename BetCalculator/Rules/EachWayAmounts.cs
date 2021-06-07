namespace BetCalculator.Rules
{
    public readonly struct EachWayAmounts
    {
        public static readonly EachWayAmounts Zero = new();
        public static readonly EachWayAmounts One = new(1m, 1m);

        public decimal Win { get; }
        public decimal Place { get; }

        public EachWayAmounts(decimal win, decimal place)
        {
            Win = win;
            Place = place;
        }

        public decimal Total => Win + Place;
        
        public static EachWayAmounts operator +(EachWayAmounts a1, EachWayAmounts a2) => 
            new(a1.Win + a2.Win, a1.Place + a2.Place);
        
        public static EachWayAmounts operator *(EachWayAmounts a1, EachWayAmounts a2) =>
            new(a1.Win * a2.Win, a1.Place * a2.Place);
    }
}