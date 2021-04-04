using Shared.Infrastructure;

namespace Order.Events
{
    public class OrderCreated : DomainEvent<OrderCreatedPayload>
    {
        public OrderCreated(OrderCreatedPayload payload) : base(payload) { }
    }
}
