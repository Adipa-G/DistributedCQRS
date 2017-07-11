using System.Threading.Tasks;
using DistCqrs.Core.Command;

namespace DistCqrs.Core.Services
{
    public interface IService<in TCmd> 
        where TCmd : ICommand
    {
        Task Process(TCmd cmd);
    }
}
