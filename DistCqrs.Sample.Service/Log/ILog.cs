using DistCqrs.Core.Services;

namespace DistCqrs.Sample.Service.Log
{
    public interface ILogStart : ILog
    {
        void Init();
    }
}
