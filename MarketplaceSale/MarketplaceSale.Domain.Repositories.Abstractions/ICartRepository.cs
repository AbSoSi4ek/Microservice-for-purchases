using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketplaceSale.Domain.Entities;

namespace MarketplaceSale.Domain.Repositories.Abstractions
{
    public interface ICartRepository : IRepository<Cart, Guid>
    {
        //Task<Cart?> GetByCartIdAsync(Guid clientId, CancellationToken cancellationToken);
    }
}
