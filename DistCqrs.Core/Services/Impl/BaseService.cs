using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DistCqrs.Core.Command;
using DistCqrs.Core.Exceptions;

namespace DistCqrs.Core.Services.Impl
{
    public abstract class BaseService : IService, IBusSubscriber
    {
        private readonly Dictionary<string,IBus> inputBuses;
        private readonly Dictionary<string,IBus> outputBuses;

        private readonly ILog log;
        private readonly ICommandProcessor commandProcessor;

        protected BaseService(ILog log, ICommandProcessor commandProcessor)
        {
            this.log = log;
            this.commandProcessor = commandProcessor;

            inputBuses = new Dictionary<string, IBus>();
            outputBuses = new Dictionary<string, IBus>();
        }

        public string Id { get; protected set; }

        public abstract IList<string> GetInputBusIds();

        public abstract IList<string> GetOutputBusIds();

        public abstract Task OnCommandProcessed(ICommand cmd);

        public abstract Task OnCommandError(ICommand cmd);

        public void RegisterInputBus(IBus bus)
        {
            if (inputBuses.ContainsKey(bus.Id))
            {
                throw new BusRegistrationException($"Input bus {bus.Id} is already registered for service {Id}");
            }
            inputBuses[bus.Id] = bus;
        }

        public void RegisterOutputBus(IBus bus)
        {
            if (inputBuses.ContainsKey(bus.Id))
            {
                throw new BusRegistrationException($"Output bus {bus.Id} is already registered for service {Id}");
            }

            outputBuses[bus.Id] = bus;
            bus.Subscribe(this);
        }

        public async Task Receive(IBus srcBus, IBusMessage message)
        {
            // ReSharper disable once SuspiciousTypeConversion.Global
            var cmd = message as ICommand;
            if (cmd == null)
            {
                log.LogError($"Error while processing message {message} in service {Id}");
            }

            try
            {
                await commandProcessor.Process(cmd);
                await OnCommandProcessed(cmd);
            }
            catch (Exception ex)
            {
                log.LogException($"Error while processing command {cmd} in service {Id}", ex);
                await OnCommandError(cmd);
            }
        }

        protected async Task SendMessage(string busId, IBusMessage message)
        {
            var bus = outputBuses[busId];
            if (bus == null)
            {
                throw new BusRegistrationException($"Bus {busId} not registered for service {Id}");
            }
            await bus.Send(message);
        }
    }
}