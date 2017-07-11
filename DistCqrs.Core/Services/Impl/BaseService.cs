using System.Threading.Tasks;
using DistCqrs.Core.Command;

namespace DistCqrs.Core.Services.Impl
{
    public abstract class BaseService<TCmd> : IService<TCmd>
        where TCmd:ICommand
    {
        private readonly ICommandProcessor<TCmd> processor;

        protected BaseService(ICommandProcessor<TCmd> processor)
        {
            this.processor = processor;
        }

        public async Task Process(TCmd cmd)
        {
            await processor.Process(cmd);
        }
    }
}
