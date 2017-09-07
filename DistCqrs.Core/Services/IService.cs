using System.Collections.Generic;

namespace DistCqrs.Core.Services
{
    public interface IService
    {
        string Id { get; }

        IList<string> GetInputBusIds();

        IList<string> GetOutputBusIds();

        void RegisterInputBus(IBus bus);

        void RegisterOutputBus(IBus bus);
    }
}