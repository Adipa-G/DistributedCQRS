using System.Linq;
using System.Threading.Tasks;
using DistCqrs.Exceptions;
using DistCqrs.Interfaces.Command;
using DistCqrs.Interfaces.Domain;
using DistCqrs.Interfaces.EventStore;
using DistCqrs.Interfaces.Services;

namespace DistCqrs.Impl.Command
{
    public class CommandProcessor : ICommandProcessor
    {
        private readonly IRootFactory rootFactory;
        private readonly IServiceLocator serviceLocator;
        private readonly IEventStore eventStore;

        public CommandProcessor(IRootFactory rootFactory,
            IServiceLocator serviceLocator,
            IEventStore eventStore)
        {
            this.rootFactory = rootFactory;
            this.serviceLocator = serviceLocator;
            this.eventStore = eventStore;
        }

        public async Task Process(ICommand cmd)
        {
            var commandHandler = serviceLocator.ResolveCommandHandler(cmd);
            if (commandHandler == null)
            {
                throw new ServiceLocationException(
                    $"Cannot resolve service to process command of type {cmd.GetType().FullName}");
            }

            var root = await GetRoot(cmd);
            var events = await commandHandler.Handle(root,cmd);
            await eventStore.SaveEvents(events);
        }

        private async Task<IRoot> GetRoot(ICommand cmd)
        {
            var events = await eventStore.GetEvents(cmd.RootId);
            if (!events.Any()) return null;

            var root = rootFactory.Create(events.First());
            foreach (var evt in events)
            {
                var evtHandler = serviceLocator.ResolveEventHandler(root, evt);
                evtHandler.Apply(root, evt);
            }
            return root;
        }
    }
}
