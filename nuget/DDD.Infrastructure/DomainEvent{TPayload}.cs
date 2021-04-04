using System;

namespace Shared.Infrastructure
{
    public class DomainEvent<TPayload> : IDomainEvent<TPayload> where TPayload :IPayload
    {
        public Guid EventId { get; set; }
        public DateTime TimeStamp { get; set; }
        public TPayload Data { get; set; }
        
        public DomainEvent(TPayload payload)
        {
            EventId = Guid.NewGuid(); // Must be a better way. EventId should be unique in all system not in domain. Hi/Lo Algorithm can be searched. https://www.baeldung.com/hi-lo-algorithm-hibernate
            TimeStamp = DateTime.UtcNow;
            Data = payload;
        }
    }
}
