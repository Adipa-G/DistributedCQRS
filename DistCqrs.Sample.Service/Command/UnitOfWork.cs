using System.Threading.Tasks;
using DistCqrs.Core.Command;

namespace DistCqrs.Sample.Service.Command
{
    public class UnitOfWork : IUnitOfWork
    {
        public void Dispose()
        {
        }

        public Task Complete()
        {
            return Task.FromResult(0);
        }
    }
}
