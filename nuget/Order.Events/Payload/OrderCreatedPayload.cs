using Shared.Infrastructure;
using System;
using System.Collections.Generic;

namespace Order.Events
{
    public class OrderCreatedPayload :IPayload
    {
        public Guid OrderId { get; set; }
        
        public List<OrderItemPayload> Items { get; set; }
        
        public OrderType OrderType { get; set; }
    }

    
}
