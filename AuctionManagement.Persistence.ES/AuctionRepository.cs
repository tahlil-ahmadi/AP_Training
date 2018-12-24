using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using AuctionManagement.Domain.Contracts;
using AuctionManagement.Domain.Framework;
using AuctionManagement.Domain.Model;
using AuctionManagement.Domain.Model.Auctions;
using EventStore.ClientAPI;
using Newtonsoft.Json;

namespace AuctionManagement.Persistence.ES
{
    public class AuctionRepository : IAuctionRepository
    {
        public Auction Get(Guid id)
        {
            var streamId = $"Auction-{id}";

            var connection = EventStoreConnection.Create(new IPEndPoint(IPAddress.Loopback, 1113));
            connection.ConnectAsync().Wait();

            var streamEvents = connection.ReadStreamEventsForwardAsync(streamId, 0, 99, false).Result;

            var auction = (Auction)Activator.CreateInstance(typeof(Auction),true);
            foreach (var streamEvent in streamEvents.Events)
            {
                var json = Encoding.UTF8.GetString(streamEvent.Event.Data);
                var type = streamEvent.Event.EventType;

                var domainEventType = Type.GetType(type);
                var instance = (DomainEvent)JsonConvert.DeserializeObject(json, domainEventType);

                auction.Apply(instance);
            }
            return auction;
        }

        public void Add(Auction auction)
        {
            var connection = EventStoreConnection.Create(new IPEndPoint(IPAddress.Loopback, 1113));
            connection.ConnectAsync().Wait();

            var events = auction.GetChanges();
            var eventData = new List<EventData>();
            foreach (var domainEvent in events)
            {
                var serializedContent = JsonConvert.SerializeObject(domainEvent);
                var item = new EventData(domainEvent.EventId, domainEvent.GetType().AssemblyQualifiedName,
                    true, Encoding.UTF8.GetBytes(serializedContent), null);

                eventData.Add(item);
            }

            connection.AppendToStreamAsync($"Auction-{auction.Id}", ExpectedVersion.Any, eventData).Wait();

            //var myEvent = new EventData(Guid.NewGuid(), "testEvent", false,
            //    Encoding.UTF8.GetBytes("some data"),
            //    Encoding.UTF8.GetBytes("some metadata"));
        }
    }
}
