using Shared.Infrastructure;
using System;

namespace Order.Domain
{
    public class OrderItem : Entity
    {
        public Guid OrderId { get; protected set; }

        public Guid ProductId { get; protected set; }

        public virtual double Price { get; protected set; }

        public virtual int Count { get; protected set; }

        //It should be only used by EF Core domain mapping stuff. Not for application usage.
        protected OrderItem() { }

        // Can also be a parametered constructor. However using a `Create` method is more readable and DDDish.
        public static OrderItem Create(Guid orderId, Guid productId, double price, int count)
        {
            if (count <= 0)
            {
                throw new Exception("Count must be positive.");
            }

            if (price <= 0)
            {
                throw new Exception("Price must be positive.");
            }

            var newOrderItem = new OrderItem()
            {
                OrderId = orderId,
                ProductId = productId,
                Count = count,
                Price = price
            };

            return newOrderItem;
        }

        public void UpdatePrice(double price)
        {
            Price = price;
        }

        internal void ChangeCount(int newCount)
        {
            Count = newCount;
        }

        public override object[] GetKeys()
        {
            return new object[] { OrderId, ProductId };
        }
    }
}
