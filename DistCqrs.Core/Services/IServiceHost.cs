namespace DistCqrs.Core.Services
{
    public interface IServiceHost
    {
        void Init(string[] serviceIds);
    }
}