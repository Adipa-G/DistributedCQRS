using Interfaces.Command;

namespace Interfaces.Services
{
    public interface IServiceLocator
    {
        ICommandHandler<T> ResolveCommandHandler<T>(T cmd) where T : ICommand;

        T ResolveEvent<T>() where T : ICommand;
    }
}
