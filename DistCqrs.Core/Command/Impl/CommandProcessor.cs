using System.Linq;
using System.Threading.Tasks;
using DistCqrs.Core.DependencyInjection;
using DistCqrs.Core.Domain;
using DistCqrs.Core.EventStore;
using DistCqrs.Core.Exceptions;

namespace DistCqrs.Core.Command.Impl
{
    public class CommandProcessor<TCmd> : ICommandProcessor<TCmd> 
        where TCmd:ICommand
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

        public async Task Process(TCmd cmd)
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
