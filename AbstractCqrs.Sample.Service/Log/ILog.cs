using AbstractCqrs.Core.Services;

namespace AbstractCqrs.Sample.Service.Log
{
    public interface ILogStart : ILog
    {
        void Init();
    }
}