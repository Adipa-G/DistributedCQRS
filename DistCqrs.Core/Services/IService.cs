using System.Threading.Tasks;
using DistCqrs.Core.Command;

namespace DistCqrs.Core.Services
{
    public interface IService
    {
        bool CanProcess(ICommand cmd);

        Task Process(ICommand cmd);
    }
}
