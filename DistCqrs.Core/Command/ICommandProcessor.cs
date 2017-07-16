using System.Threading.Tasks;
using DistCqrs.Core.Domain;

namespace DistCqrs.Core.Command
{
    public interface ICommandProcessor<TRoot, in TCmd>
        where TRoot : IRoot
        where TCmd : ICommand<TRoot>
    {
        Task Process(TCmd cmd);
    }
}