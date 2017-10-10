using System.Threading.Tasks;

namespace AbstractCqrs.Core.Command
{
    public interface ICommandProcessor
    {
        Task Process(ICommand cmd);
    }
}