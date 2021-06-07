using System;
using BetCalculator.Rules;
using static BetCalculator.Rules.EachWayType;

namespace BetCalculator
{
   public record LegStatus
   {
      public static readonly LegStatus Open = new(1m, 0m, 1m, false);
      public static readonly LegStatus Won = new(1m, 0m, 1m, true);
      public static readonly LegStatus Void = new(0m, 1m, 1m, true);
      public static readonly LegStatus Lost = new(0m, 0m, 1m, true);
      
      public static LegStatus OpenEw(decimal placeOddsFactor) =>
         new(1m, 0m, placeOddsFactor, false);

      public decimal WinFactor { get; }
      public decimal VoidFactor { get; }
      public decimal PlaceOddsFactor { get; }
      public BetState State { get; }

      public LegStatus(decimal winFactor, decimal voidFactor, decimal placeOddsFactor, bool resulted)
      {
         if (winFactor < 0m)
            throw new ArgumentException("winFactor must not be negative");
         if (voidFactor < 0m)
            throw new ArgumentException("voidFactor must not be negative");
         if (placeOddsFactor < 0m)
            throw new ArgumentException("placeOddsFactor must not be negative");
         WinFactor = winFactor;
         VoidFactor = voidFactor;
         PlaceOddsFactor = placeOddsFactor;
         if (!resulted)
            State = BetState.Open;
         else if (winFactor > 0m)
            State = BetState.Won;
         else if (voidFactor > 0m)
            State = BetState.Void;
         else
            State = BetState.Lost;
      }

      public decimal FactoredPrice(decimal price, EachWayType eachWayType)
         => WinFactor * ApplyOddsFactor(price, eachWayType) + VoidFactor;

      private decimal ApplyOddsFactor(decimal price, EachWayType eachWayType)
         => eachWayType switch
         {
            Win => price,
            Place => 1m + (price - 1m) * PlaceOddsFactor,
            _ => throw new ArgumentOutOfRangeException(nameof(eachWayType), eachWayType, "Invalid eachWayType for LegStatus")
         };
   }
}