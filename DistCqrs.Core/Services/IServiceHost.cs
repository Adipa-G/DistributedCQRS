namespace DistCqrs.Core.Services
{
    public interface IServiceHost
    {
        void Register<T>() where T : IService;
    }
}
