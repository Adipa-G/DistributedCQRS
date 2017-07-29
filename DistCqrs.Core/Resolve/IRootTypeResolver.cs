using System;
using DistCqrs.Core.Command;

namespace DistCqrs.Core.Resolve
{
    public interface IRootTypeResolver
    {
        Type GetRootType(ICommand cmd);
    }
}
