using System;
using AuctionManagement.Application;
using AuctionManagement.Application.Contracts;
using AuctionManagement.Domain.Model.Auctions;
using AuctionManagement.Gateways.RestApi;
using AuctionManagement.Persistence.ES;
using Framework.Application;
using Framework.Core;
using Framework.SimpleInjector;
using SimpleInjector;

namespace AuctionManagement.Config
{
    public static class AuctionConfigurator
    {
        public static void WireUp(Container container)
        {
            container.Register(typeof(ICommandHandler<>), typeof(AuctionCommandHandlers).Assembly, Lifestyle.Scoped);
            container.Register(typeof(IAuctionRepository), typeof(AuctionRepository), Lifestyle.Scoped);
            container.Register(typeof(AuctionsController));


            //should move to framework.configuration
            container.Register(typeof(ICommandBus),typeof(CommandBus));
            container.Register(typeof(IServiceLocator),typeof(SimpleInjectorAdapter));
        }
    }
}
