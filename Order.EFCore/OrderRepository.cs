using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Order.EFCore
{
    public interface IOrderRepository
    {
        Task SaveAsync(Domain.Order order);
        Task<List<Domain.Order>> GetOrdersByProductId(Guid productId);
    }
}
