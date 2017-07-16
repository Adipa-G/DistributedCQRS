using System.Threading.Tasks;
using DistCqrs.Core.Command;
using DistCqrs.Core.Domain;

namespace DistCqrs.Core.Services.Impl
{
    public abstract class BaseService<TRoot, TCmd> : IService<TRoot, TCmd>
        where TRoot : IRoot
        where TCmd : ICommand<TRoot>
    {
        private readonly ICommandProcessor<TRoot, TCmd> processor;

        protected BaseService(ICommandProcessor<TRoot, TCmd> processor)
        {
            this.processor = processor;
        }

        public async Task Process(TCmd cmd)
        {
            await processor.Process(cmd);
        }
    }
}