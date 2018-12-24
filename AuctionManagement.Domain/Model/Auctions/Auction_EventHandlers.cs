using System;
using System.Collections.Generic;
using System.Text;
using AuctionManagement.Domain.Contracts;
using AuctionManagement.Domain.Contracts.Events;
using AuctionManagement.Domain.Framework;

namespace AuctionManagement.Domain.Model.Auctions
{
    public partial class Auction
    {
        public override void Apply(DomainEvent @event)
        {
            this.Apply((dynamic)@event);
        }
        private void Apply(AuctionOpened @event)
        {
            this.Id = @event.Id;
            this.EndDateTime = @event.EndDateTime;
            this.ProductDescription = @event.ProductDescription;
            this.SellerId = @event.SellerId;
            this.StartingPrice = @event.StartingPrice;
        }
        private void Apply(BidPlaced @event)
        {
            this.WinningBid = new Bid()
            {
                Amount = @event.Amount,
                BidderId = @event.BidderId,
                OfferDateTime = @event.OfferDateTime
            };
        }
    }
}
