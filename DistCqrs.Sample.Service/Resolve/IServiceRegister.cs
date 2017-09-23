using DistCqrs.Core.Services;

namespace DistCqrs.Sample.Service.Resolve
{
    public interface IServiceRegister
    {
        void Register(IBus bus);

        void Register(IService service);
    }
}