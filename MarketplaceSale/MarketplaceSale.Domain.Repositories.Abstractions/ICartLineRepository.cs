using MarketplaceSale.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceSale.Domain.Repositories.Abstractions
{
    
    public interface ICartLineRepository : IRepository<CartLine, Guid>
    {
        /*
        Task<CartLine?> GetByCartIdAndProductIdAsync(Guid cartId, Guid productId, CancellationToken cancellationToken = default);

        Task<IEnumerable<CartLine>> GetAllByCartIdAsync(Guid cartId, CancellationToken cancellationToken = default);

        Task<CartLine> AddAsync(CartLine cartLine, CancellationToken cancellationToken = default);

        Task<bool> UpdateAsync(CartLine cartLine, CancellationToken cancellationToken = default);

        Task<bool> DeleteAsync(CartLine cartLine, CancellationToken cancellationToken = default);

        Task<bool> ExistsAsync(Guid cartId, Guid productId, CancellationToken cancellationToken = default);*/
    }
    
}

