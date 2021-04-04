using System;

namespace Shared.Infrastructure
{
    public interface IDomainEvent<TPayload> : IEvent where TPayload : IPayload
    {
        TPayload Data { get; set; }
    }
}
