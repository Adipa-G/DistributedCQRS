using System.Threading.Tasks;
using DistCqrs.Core.Command;

namespace DistCqrs.Core.Services.Impl
{
    public abstract class BaseService : IService
    {
        private readonly ICommandProcessor processor;

        protected BaseService(ICommandProcessor processor)
        {
            this.processor = processor;
        }

        public abstract bool CanProcess(ICommand cmd);

        public async Task Process(ICommand cmd)
        {
            await processor.Process(cmd);
        }
    }
}
