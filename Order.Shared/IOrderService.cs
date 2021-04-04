using Order.Events;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Order.Shared
{
    public interface IOrderService
    {
        Task<OrderDto> CreateOrder(OrderType orderType, List<OrderItemDto> orderItems);
        
        Task AddItemToOrder(OrderItemDto orderItem);
        
        Task RemoveItemFromOrder(Guid productId);

        Task IncreateAmountOfOrderItem(Guid orderId, Guid productId, int increment);
    }
}
