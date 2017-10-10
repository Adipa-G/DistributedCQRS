using System;
using System.Threading.Tasks;

namespace AbstractCqrs.Core.Command
{
    public interface IUnitOfWork : IDisposable
    {
        Task Complete();
    }
}