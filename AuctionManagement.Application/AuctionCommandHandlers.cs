using System;
using AuctionManagement.Application.Contracts;
using AuctionManagement.Domain.Model.Auctions;
using Framework.Application;

namespace AuctionManagement.Application
{
    public class AuctionCommandHandlers : ICommandHandler<PlaceBidCommand>,
                                          ICommandHandler<OpenAuctionCommand>
    {
        private readonly IAuctionRepository _repository;
        public AuctionCommandHandlers(IAuctionRepository repository)
        {
            _repository = repository;
        }

        public void Handle(PlaceBidCommand command)
        {
        }

        public void Handle(OpenAuctionCommand command)
        {
            var auction = new Auction(command.StartingPrice,command.SellerId, command.EndDateTime, command.ProductDescription);
            _repository.Add(auction);
        }
    }
}
