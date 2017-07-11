using System.Threading.Tasks;
using DistCqrs.Core.Command;

namespace DistCqrs.Core.Services
{
    public interface IServiceHost
    {
        Task CommandReceived<TCmd>(TCmd cmd)
            where TCmd : ICommand;
    }
}
