using System.Collections.Generic;
using System.Threading.Tasks;
using DistCqrs.Core.Domain;

namespace DistCqrs.Core.Command
{
    public interface ICommandHandler<in TCmd> 
        where TCmd:ICommand
    {
        Task<IList<IEvent>> Handle(IRoot root, TCmd cmd);
    }
}
