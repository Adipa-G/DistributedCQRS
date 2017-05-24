using System.Threading.Tasks;
using Interfaces.Command;
using Interfaces.Services;

namespace Domain.Command
{
    public class CommandProcessor
    {
        private readonly IServiceLocator serviceLocator;

        public CommandProcessor(IServiceLocator serviceLocator)
        {
            this.serviceLocator = serviceLocator;
        }

        public async Task<ICommandResult> Process(ICommand cmd)
        {
            var commandHandler = serviceLocator.ResolveCommandHandler(cmd);
            var events = await commandHandler.Handle(cmd);

        }
    }
}
