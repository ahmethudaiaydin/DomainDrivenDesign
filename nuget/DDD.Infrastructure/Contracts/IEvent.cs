using System;

namespace Shared.Infrastructure
{
    public interface IEvent
    {
        Guid EventId { get; set; }
        DateTime TimeStamp { get; set; }
    }
}
