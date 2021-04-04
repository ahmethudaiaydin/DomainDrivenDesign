using System;
using System.Collections.Generic;
using System.Linq;
using Order.Events;
using Shared.Infrastructure;

namespace Order.Domain
{
    public class Order : AggregateRoot<Guid>
    {
        // Why private fields & IReadOnlyCollection?
        // https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/infrastructure-persistence-layer-implementation-entity-framework-core
        private List<OrderItem> _orderItems;

        public double TotalPrice => _orderItems.Sum(x => x.Price * x.Count);

        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

        public OrderType OrderType { get; set; }

        public Guid CustomerId { get; set; }


        //FOR EF initialization
        protected Order () { }

        // Create aggregate root with that method. It can also be a constructor.
        public static Order Create(Guid id, List<OrderItem> orderItems, OrderType type)
        {
            var order = new Order()
            {
                Id = id,
                _orderItems = orderItems,
                OrderType = type
            };

            var payload = new OrderCreatedPayload
            {
                OrderId = id,
                OrderType = type,
                Items = orderItems.Select(x => new OrderItemPayload() { OrderId = id, Price = x.Price, Count = x.Count, ProductId = x.ProductId }).ToList()
            };

            RegisterEvent(new OrderCreated(payload));

            // *** Generic usage. If we do not make any changes on domain event, then we don't need to write domain event like OrderCreated. 
            // *** But just in case, tracing can be hard in a generic way. In logs, there will not be OrderCreated event, but DomainEvent with a payload OrderCreatedPayload.
            
            // RegisterEvent(new DomainEvent<OrderCreatedPayload>(payload));

            return order;
        }

        public void AddItem(Guid productId, double price, int count)
        {
            var item = OrderItem.Create(Id, productId, price, count);
            _orderItems.Add(item);
        }

        public void RemoveItem(Guid productId)
        {
            _orderItems.RemoveAt(_orderItems.FindIndex(x => x.ProductId.Equals(productId)));
        }

        public void IncreaseAmount(Guid productId, int increment)
        {
            var itemToChange = _orderItems.Find(x => x.ProductId.Equals(productId));
            if (itemToChange == null)
            {
                throw new Exception("Item not found");
            }

            itemToChange.ChangeCount(itemToChange.Count + increment);

            _orderItems[_orderItems.FindIndex(x => x.ProductId.Equals(itemToChange.ProductId))] = itemToChange;

        }

        public void UpdatePrice(Guid productId, double price)
        {
            var itemToChange = _orderItems.Find(x => x.ProductId.Equals(productId));
            if (itemToChange == null)
            {
                throw new Exception("Item not found");
            }

            itemToChange.UpdatePrice(price);

            _orderItems[_orderItems.FindIndex(x => x.ProductId.Equals(itemToChange.ProductId))] = itemToChange;
        }

    }
}
