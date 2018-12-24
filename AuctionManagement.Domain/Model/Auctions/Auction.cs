using System;
using AuctionManagement.Domain.Contracts.Events;
using AuctionManagement.Domain.Framework;

namespace AuctionManagement.Domain.Model.Auctions
{
    public partial class Auction : AggregateRoot
    {
        public Guid Id { get; private set; }
        public long StartingPrice { get; private set; }
        public long SellerId { get; private set; }
        public string ProductDescription { get; private set; }
        public DateTime EndDateTime { get; private set; }
        public Bid WinningBid { get; private set; }

        protected Auction() { }
        public Auction(long startingPrice, long sellerId, DateTime endDateTime, string productDescription)
        {
            if (endDateTime <= DateTime.Now) throw new Exception();

            //this.Id = Guid.NewGuid();
            //this.StartingPrice = startingPrice;
            //this.SellerId = sellerId;
            //this.EndDateTime = endDateTime;
            //this.ProductDescription = productDescription;

            Causes(new AuctionOpened(Guid.NewGuid(), startingPrice, sellerId, endDateTime, productDescription));
        }
        public void PlaceBid(Bid bid)
        {
            var maxBid = this.StartingPrice;
            if (this.WinningBid != null) maxBid = WinningBid.Amount;
            if (maxBid >= bid.Amount) throw new Exception();

            if (bid.BidderId == this.SellerId) throw new Exception();

            Causes(new BidPlaced(this.Id, bid.BidderId, bid.Amount, bid.OfferDateTime));
        }
    }
}
