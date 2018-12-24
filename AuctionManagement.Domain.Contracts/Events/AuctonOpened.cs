using System;

namespace AuctionManagement.Domain.Contracts.Events
{
    public class AuctionOpened : DomainEvent
    {
        public Guid Id { get; private set; }
        public long StartingPrice { get; private set; }
        public long SellerId { get; private set; }
        public DateTime EndDateTime { get; private set; }
        public string ProductDescription { get; private set; }
        public AuctionOpened(Guid id, long startingPrice, long sellerId, DateTime endDateTime,
            string productDescription)
        {
            this.Id = id;
            this.StartingPrice = startingPrice;
            this.SellerId = sellerId;
            this.EndDateTime = endDateTime;
            this.ProductDescription = productDescription;
        }
    }
}
