using Shared.Infrastructure;
using System;

namespace Order.Events
{
    public class OrderItemPayload : IPayload
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public double Price { get; set; }
        public int Count { get; set; }
    }
}
