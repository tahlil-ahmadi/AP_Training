using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AuctionManagement.Domain.Contracts;
using EventStore.ClientAPI;
using EventStore.ClientAPI.SystemData;

namespace AuctionManagement.QueryModelSync
{
    class Program
    {
        static void Main(string[] args)
        {
            var connection = EventStoreConnection.Create(new IPEndPoint(IPAddress.Loopback, 1113));
            connection.ConnectAsync().Wait();
            var credential = new UserCredentials("admin", "changeit");

            connection.SubscribeToAllAsync(true, EventAppeared, SubscriptionDropped,credential).Wait();

            Console.WriteLine("Subscription started");
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
                Console.WriteLine(Encoding.UTF8.GetString(arg2.Event.Data));
            }
            return Task.CompletedTask;
        }
    }
}
