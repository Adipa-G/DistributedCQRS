using System.Collections.Generic;

namespace DistCqrs.Core.Services
{
    public interface IService
    {
        string Id { get; }

        void Init();
    }
}