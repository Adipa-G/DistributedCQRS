using System.Threading.Tasks;

namespace DistCqrs.Interfaces.Command
{
    public interface ICommandProcessor
    {
        Task Process(ICommand cmd);
    }
}
