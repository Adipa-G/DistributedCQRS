using System.Collections.Generic;
using System.Threading.Tasks;

namespace AbstractCqrs.Core.Services.Impl
{
    public class InternalBus : IBus
    {
        private readonly IList<IBusSubscriber> subscribers;

        public InternalBus(string id)
        {
            Id = id;
            subscribers = new List<IBusSubscriber>();
        }

        public string Id { get; }

        public void Subscribe(IBusSubscriber subscriber)
        {
            subscribers.Add(subscriber);
        }

        public async Task Send(IBusMessage message)
        {
            foreach (var subscriber in subscribers)
                await subscriber.Receive(this, message);
        }
    }
}