namespace AbstractCqrs.Core.Services
{
    public interface IServiceHost
    {
        void Init(string[] serviceIds);
    }
}