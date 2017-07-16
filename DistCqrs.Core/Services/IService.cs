using System.Threading.Tasks;
using DistCqrs.Core.Command;
using DistCqrs.Core.Domain;

namespace DistCqrs.Core.Services
{
    public interface IService<TRoot, in TCmd>
        where TRoot : IRoot
        where TCmd : ICommand<TRoot>
    {
        Task Process(TCmd cmd);
    }
}