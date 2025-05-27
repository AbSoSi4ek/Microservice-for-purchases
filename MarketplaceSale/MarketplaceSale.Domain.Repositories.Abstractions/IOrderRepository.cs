using MarketplaceSale.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceSale.Domain.Repositories.Abstractions
{
    public interface IOrderRepository : IRepository<Order, Guid>
    {
        Task<IReadOnlyList<Order>> GetByOrderIdAsync(Guid orderId, CancellationToken cancellationToken = default);
    }
}
