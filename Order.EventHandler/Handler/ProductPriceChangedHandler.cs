using Order.Shared;
using Product.Events.Payload;

namespace Order.EventHandler
{
    // Product will be come from another shared nuget

    public class ProductPriceChangedHandler
    {
        private readonly IOrderService _orderService;
        public ProductPriceChangedHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async void Handle(ProductPayload payload)
        {
             await _orderService.ChangeProductPrice(payload.Id, payload.Price); 
        }
    }
}
