using System;

namespace Interfaces.EventStore
{
    public interface IEventRecord
    {
        Guid Id { get; }

        Guid RootId { get; }

        int SeqNo { get; }

        string Data { get; }
    }
}
