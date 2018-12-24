using System;
using AuctionManagement.Domain.Model;
using AuctionManagement.Domain.Model.Auctions;
using Xunit;

namespace AuctionManagement.Persistence.ES.Tests.Integration
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var rep = new AuctionRepository();

            #region Save
            var auction = new Auction(1000, 10, DateTime.Now.AddDays(7), "Black shoes");
            auction.PlaceBid(new Bid() { Amount = 1100, BidderId = 12, OfferDateTime = DateTime.Now });
            auction.PlaceBid(new Bid() { Amount = 1200, BidderId = 23, OfferDateTime = DateTime.Now });

            rep.Add(auction);
            #endregion

            //var auction = rep.Get(Guid.Parse("62dd1efc-cace-4098-845c-1d87d013009a"));
        }
    }
}
