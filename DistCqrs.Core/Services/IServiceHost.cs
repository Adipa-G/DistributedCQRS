using System.Threading.Tasks;
using DistCqrs.Core.Command;
using DistCqrs.Core.Domain;

namespace DistCqrs.Core.Services
{
    public interface IServiceHost
    {
        Task CommandReceived<TRoot, TCmd>(TCmd cmd)
            where TRoot : IRoot, new()
            where TCmd : ICommand;
    }
}