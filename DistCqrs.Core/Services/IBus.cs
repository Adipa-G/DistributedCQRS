using System.Threading.Tasks;

namespace DistCqrs.Core.Services
{
    public interface IBus
    {
        string Id { get; }

        void Subscribe(IBusSubscriber subscriber);

        Task Send(IBusMessage message);
    }
}