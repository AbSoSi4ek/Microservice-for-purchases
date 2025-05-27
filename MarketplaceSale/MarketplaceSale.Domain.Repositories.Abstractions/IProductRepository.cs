using MarketplaceSale.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceSale.Domain.Repositories.Abstractions
{
    public interface IProductRepository : IRepository<Product, Guid>
    {
        Task<IEnumerable<Product>> GetByProductIdAsync(Guid productId, CancellationToken cancellationToken = default);
    }
}
