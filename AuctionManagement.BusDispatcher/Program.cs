using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AuctionManagement.Domain.Contracts;
using EventStore.ClientAPI;
using EventStore.ClientAPI.SystemData;
using Newtonsoft.Json;
using NServiceBus;
using NServiceBus.Routing;

namespace AuctionManagement.BusDispatcher
{
    class Program
    {
        private static IEndpointInstance bus;
        static void Main(string[] args)
        {
            bus = EndpointConfig.Config();

            var connection = EventStoreConnection.Create(new IPEndPoint(IPAddress.Loopback, 1113));
            connection.ConnectAsync().Wait();
            var credential = new UserCredentials("admin", "changeit");

            connection.SubscribeToAllAsync(true, EventAppeared, SubscriptionDropped,credential).Wait();

            Console.WriteLine("Subscription started...");
            Console.ReadLine();
        }

        private static void SubscriptionDropped(EventStoreSubscription arg1, SubscriptionDropReason arg2, Exception arg3)
        {
        }

        private static Task EventAppeared(EventStoreSubscription arg1, ResolvedEvent arg2)
        {
            var type = Type.GetType(arg2.Event.EventType);
            if (type != null && typeof(DomainEvent).IsAssignableFrom(type))
            {
                var json = Encoding.UTF8.GetString(arg2.Event.Data);
                var @event = JsonConvert.DeserializeObject(json, type);

                bus.Publish(@event).Wait();
                Console.WriteLine("Dispatched on bus..");
                Console.WriteLine("==================================================");
            }
            return Task.CompletedTask;
        }
    }
}
