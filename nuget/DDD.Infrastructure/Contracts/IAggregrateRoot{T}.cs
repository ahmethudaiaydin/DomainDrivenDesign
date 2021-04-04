using System;
using System.Collections.Generic;

namespace Shared.Infrastructure
{
    public interface IAggregateRoot<T>
    {
        T Id { get; }

        IReadOnlyCollection<IEvent> Events { get; }
    }
}
