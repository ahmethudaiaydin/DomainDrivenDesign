using System;
using System.Collections.Generic;

namespace Order.Shared
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public double TotalPrice { get; set; }
        public List<OrderItemDto> Items { get; set; }
    }
}
