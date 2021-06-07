using System;
using System.Collections;

namespace BetCalculator.Interrelation
{
    public record IrResult
    {
        public static readonly IrResult NotInterrelated = new(IrType.NotInterrelated);
        public IrType IrType { get; } 
        public string Message { get; }

        protected IrResult(IrType irType, string? message = null)
        {
            IrType = irType;
            Message = message;
        }

        public bool IsInterrelated() => IrType != IrType.NotInterrelated;

        public virtual IrException ToIrException() => throw new NotImplementedException();
    }

    public record IrSelectionsResult : IrResult
    {
        public object SelectionId1 { get; }
        public object SelectionId2 { get; }

        public IrSelectionsResult(IrType irType, object selectionId1, object selectionId2, string reason) :
            base(irType, $"Selections {selectionId1} and {selectionId2} are interrelated: {reason}")
        {
            SelectionId1 = selectionId1;
            SelectionId2 = selectionId2;
        }

        public override IrException ToIrException() => new IrSelectionsException(IrType, SelectionId1, SelectionId1, Message);
    }

    public record MaxWinnersViolationIrResult : IrResult
    {
        public object MarketId { get; }
        public IEnumerable SelectionIds { get; }
        public int MaxWinners { get; }

        public MaxWinnersViolationIrResult(object marketId, IEnumerable selectionIds, int maxWinners) :
            base(IrType.MaxWinners,
                $"MaxWinners violation: More then {maxWinners} Selections ({selectionIds}) from Market {marketId} are in the bet unit")
        {
            MarketId = marketId;
            SelectionIds = selectionIds;
            MaxWinners = maxWinners;
        }

        public override IrException ToIrException() => new MaxWinnersViolationException(IrType, MarketId, SelectionIds, Message);
    }
}