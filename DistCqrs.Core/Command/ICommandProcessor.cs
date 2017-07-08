using System.Threading.Tasks;

namespace DistCqrs.Core.Command
{
    public interface ICommandProcessor
    {
        Task Process(ICommand cmd);
    }
}
