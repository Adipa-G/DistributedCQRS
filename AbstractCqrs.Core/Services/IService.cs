namespace AbstractCqrs.Core.Services
{
    public interface IService
    {
        string Id { get; }

        void Init();
    }
}