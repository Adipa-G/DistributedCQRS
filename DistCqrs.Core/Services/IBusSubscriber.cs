using System.Threading.Tasks;

namespace DistCqrs.Core.Services
{
    public interface IBusSubscriber
    {
        Task Receive(IBus srcBus,IBusMessage message);
    }
}
