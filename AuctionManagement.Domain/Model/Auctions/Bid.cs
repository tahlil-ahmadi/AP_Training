using System;

namespace AuctionManagement.Domain.Model.Auctions
{
    public class Bid
    {
        public long BidderId { get; set; }
        public long Amount { get; set; }
        public DateTime OfferDateTime { get; set; }
    }
}