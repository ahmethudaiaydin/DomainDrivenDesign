using Order.EFCore;
using System;

namespace Order.Application
{
    // Product will be come from another shared nuget
    public class ProductPayload
    {
        public Guid Id { get; set; }
        public double Price { get; set; }

    }
    public class ProductPriceChangedHandler
    {
        private readonly IOrderRepository _orderRepository;
        public ProductPriceChangedHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        // Product will be come from another shared nuget
        public async void Handle(ProductPayload payload)
        {

            var orders = await _orderRepository.GetOrdersByProductId(payload.Id);
            
            orders.ForEach(order =>
            {
                order.UpdatePrice(payload.Id, payload.Price);
                _orderRepository.SaveAsync(order);
            });


        }
    }
}
