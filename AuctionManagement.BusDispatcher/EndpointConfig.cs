using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagement.Domain.Contracts;
using NServiceBus;

namespace AuctionManagement.BusDispatcher
{
    public static class EndpointConfig
    {
        public static IEndpointInstance Config()
        {
            var config = new EndpointConfiguration("AuctionManagement");
            config.UseTransport<MsmqTransport>();
            config.UsePersistence<InMemoryPersistence>();
            config.EnableInstallers();
            config.SendFailedMessagesTo("AuctionManagement.errors");
            config.Conventions().DefiningEventsAs(a => typeof(DomainEvent).IsAssignableFrom(a));

            return Endpoint.Start(config).Result;
        }
    }
}
