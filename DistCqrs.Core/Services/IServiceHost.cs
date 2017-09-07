namespace DistCqrs.Core.Services
{
    public interface IServiceHost
    {
        void RegisterService(IService service);

        void PrepareExternalEntpoints();

        void InitialiseServices();
    }
}