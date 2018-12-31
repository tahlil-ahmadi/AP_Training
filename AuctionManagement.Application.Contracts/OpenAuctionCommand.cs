using System;

namespace AuctionManagement.Application.Contracts
{
    public class OpenAuctionCommand
    {
        public long StartingPrice { get;  set; }
        public long SellerId { get;  set; }
        public string ProductDescription { get;  set; }
        public DateTime EndDateTime { get;  set; }
    }
}
