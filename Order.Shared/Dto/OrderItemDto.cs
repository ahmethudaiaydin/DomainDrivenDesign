using System;

namespace Order.Shared
{
    public class OrderItemDto
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public double Price { get; set; }
        public int Count { get; set; }
    }
}
