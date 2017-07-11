using System.Threading.Tasks;

namespace DistCqrs.Core.Command
{
    public interface ICommandProcessor<in TCmd> 
        where TCmd:ICommand
    {
        Task Process(TCmd cmd);
    }
}
