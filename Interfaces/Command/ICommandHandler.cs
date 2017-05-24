using System.Collections.Generic;
using System.Threading.Tasks;
using Interfaces.Events;

namespace Interfaces.Command
{
    public interface ICommandHandler<TCommand> 
        where TCommand : ICommand 
    {
        Task<IList<IEvent>> Handle(TCommand cmd);
    }
}
