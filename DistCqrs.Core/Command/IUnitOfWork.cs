using System;
using System.Threading.Tasks;

namespace DistCqrs.Core.Command
{
    public interface IUnitOfWork : IDisposable
    {
        Task Complete();
    }
}
