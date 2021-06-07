using System;
using System.Collections;

namespace BetCalculator.Interrelation
{
    public class IrException : Exception
    {
        public IrType IrType { get; }

        public IrException(IrType irType, string message) : base(message)
        {
            IrType = irType;
        }
    }

    public class IrSelectionsException : IrException
    {
        public object SelectionId1 { get; }
        public object SelectionId2 { get; }

        public IrSelectionsException(IrType irType, object selectionId1, object selectionId2, string message) : base(irType, message)
        {
            SelectionId1 = selectionId1;
            SelectionId2 = selectionId2;
        }
    }

    public class MaxWinnersViolationException : IrException
    {
        public object MarketId { get; }
        public IEnumerable SelectionIds { get; }

        public MaxWinnersViolationException(IrType irType, object marketId, IEnumerable selectionIds, string message) : base(irType, message)
        {
            MarketId = marketId;
            SelectionIds = selectionIds;
        }
    }
}