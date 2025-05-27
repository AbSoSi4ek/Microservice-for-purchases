using MarketplaceSale.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceSale.Domain.Repositories.Abstractions
{
    public interface ISellerRepository : IRepository<Seller, Guid>
    {
        Task<Seller?> GetSellerByUsernameAsync(string username, CancellationToken cancellationToken);
    }
}
