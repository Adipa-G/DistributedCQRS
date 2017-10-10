using System.Threading.Tasks;

namespace AbstractCqrs.Core.Services
{
    public interface IBusSubscriber
    {
        Task Receive(IBus srcBus, IBusMessage message);
    }
}