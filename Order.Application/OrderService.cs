using Order.Shared;
using System;
using System.Collections.Generic;
using Order.Domain;
using System.Linq;
using Order.EFCore;
using System.Threading.Tasks;
using Order.Events;

namespace Order.Application
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<OrderDto> CreateOrder(OrderType orderType, List<OrderItemDto> orderItems)
        {
            var orderId = Guid.NewGuid(); // Must be a more enique way like hi/lo algorithm.

            var createdOrder = Domain.Order.Create(orderId, orderItems.Select(x => OrderItem.Create(orderId, x.ProductId, x.Price, x.Count)).ToList(), orderType);

            // Domain events will be published to queue when EF SaveChanges called. 
            // Then we can use Transactional Outbox Pattern to persist events in Order domain boundary.
            await _orderRepository.SaveAsync(createdOrder);

            return new OrderDto()
            {
                Id = orderId,
                Items = orderItems,
            };
        }

        public async Task ChangeProductPrice(Guid productId, double price)
        {
            var orders = await _orderRepository.GetOrdersByProductId(productId);

            orders.ForEach(order =>
            {
                order.UpdatePrice(productId, price);
                _orderRepository.SaveAsync(order);
            });
        }

        public async Task AddItemToOrder(OrderItemDto orderItem)
        {
            throw new NotImplementedException();
        }

        public async Task IncreateAmountOfOrderItem(Guid orderId, Guid productId, int increment)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveItemFromOrder(Guid productId)
        {
            throw new NotImplementedException();
        }


    }
}
