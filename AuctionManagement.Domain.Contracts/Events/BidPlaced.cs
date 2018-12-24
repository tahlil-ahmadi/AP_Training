using System;

namespace AuctionManagement.Domain.Contracts.Events
{
    public class BidPlaced : DomainEvent
    {
        public Guid AuctionId { get; private set; }
        public long BidderId { get; private set; }
        public long Amount { get; private set; }
        public DateTime OfferDateTime { get; private set; }

        public BidPlaced(Guid auctionId, long bidderId, long amount, DateTime offerDateTime)
        {
            AuctionId = auctionId;
            BidderId = bidderId;
            Amount = amount;
            OfferDateTime = offerDateTime;
        }
    }
}